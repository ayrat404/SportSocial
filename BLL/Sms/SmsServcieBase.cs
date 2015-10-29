using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Security.Tokens;
using System.Threading.Tasks;
using System.Web;
using BLL.Common.Objects;
using BLL.Sms.Impls;
using DAL;
using DAL.DomainModel;
using Elmah;
using Knoema.Localization;
using Microsoft.AspNet.Identity;
using Twilio;

namespace BLL.Sms
{
    public class SmsService: ISmsService
    {
        private EntityDbContext _db;

        public SmsService(EntityDbContext db)
        {
            _db = db;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }

        public bool TimeNotExpired(int userId)
        {
            var sms = _db.SmsCodes.Where(s => s.UserId == userId
                                              && s.Expired > DateTime.Now
                                             ).ToList();
            return false;
        }

        public bool CanResendCode(int userId)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult GenerateAndSendCode(int userId, string phoneNumber)
        {
            var result = new ServiceResult {Success = false};
            var sms = _db.SmsCodes
                .Where(s => s.UserId == userId)
                .Include(s => s.Smses)
                .OrderByDescending(s => s.Created)
                .FirstOrDefault();
            if (sms != null && sms.Expired > DateTime.Now && !sms.Verified)
            {
                if ((DateTime.Now - sms.RetryTime).Seconds <= 0)
                {
                    result.ErrorMessage = "Время для повторной отправки смс не наступило".Resource(this);
                    return result;
                }
                var msg = GenerateMessage(sms.Code);
                var sendResult = SendMessage(msg, phoneNumber, sms);
                sms.Modified = DateTime.Now;
                sms.RetryTime = DateTime.Now.AddSeconds(20);
                _db.SaveChanges();
                if (!sendResult.Success)
                {
                    return sendResult;
                }
                result.Success = true;
                return result;
            }
            else
            {
                string code = GenerateCode();
                sms = new SmsCode()
                {
                    Code = code,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Expired = DateTime.Now.AddMinutes(20),
                    RetryTime = DateTime.Now.AddSeconds(20),
                    UserId = userId,
                    Deleted = false,
                };
                var msg = GenerateMessage(code);
                var sendResult = SendMessage(msg, phoneNumber, sms);
                _db.SmsCodes.Add(sms);
                _db.SaveChanges();
                if (!sendResult.Success)
                {
                    return sendResult;
                }
                result.Success = true;
                return result;
            }
        }

        public ServiceResult VerifyCode(int userId, string code)
        {
            var result = new ServiceResult {Success = false};
            var sms = _db.SmsCodes.Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Created)
                .FirstOrDefault();
            if (sms == null)
            {
                result.ErrorMessage = "Не найдено смс".Resource(this);
                return result;
            }
            if (sms.Verified)
            {
                return ServiceResult.SuccessResult();
            }
            if (sms.Expired < DateTime.Now)
            {
                result.ErrorMessage = "Время ввода смс истекло. Повторите запрос.".Resource(this);
                return result;
            }
            if (sms.Code == code)
            {
                sms.Verified = true;
                _db.SaveChanges();
                result.Success = true;
                return result;
            }
            result.ErrorMessage = "Введен неверный код.".Resource(this);
            return result;
        }

        public ServiceResult SendMessage(string msg, string phoneNumber, SmsCode smsCode)
        {
            ISmsSender sender;
            SmsProviderType providerType;
            if (!smsCode.Smses.Any())
            {
                providerType = SmsProviderType.SmsPilot;
                sender = new SmsPilotSmsService();
            }
            else
            {
                providerType = SmsProviderType.Twilio;
                sender = new TwilioSmsSender();
            }
            string externalId = string.Empty;
            #if !DEBUG
            var sendResult = sender.SendMessage(msg, phoneNumber);
            if (!sendResult.Success)
            {
                return sendResult;
            }
            externalId = sendResult.Result;
            #endif
            smsCode.Smses.Add(new SmsMessage()
            {
                Message = msg,
                Phone = phoneNumber,
                ExternalId = externalId,
                SmsProvider = (int)providerType
            });
            return ServiceResult.SuccessResult();
        }

        private string GenerateCode()
        {
#if DEBUG
                return "1111";
#endif
#if !DEBUG
            int code = new Random().Next(1000, 9999);
            return code.ToString();
#endif
        }

        private string GenerateMessage(string code = null)
        {
            if (string.IsNullOrEmpty(code))
                code = GenerateCode();
            var msg = string.Format("Код проверки телефона: {0}".Resource(this), code);
            return msg;
        }
    }

    public enum SmsProviderType
    {
        SmsPilot,
        Twilio
    }

    public interface ISmsSender
    {
        ServiceResult<string> SendMessage(string msg, string phoneNumber);
    }

    public static class SmsErrorCodes
    {
        public const int NotPhoneNumber = 1;
    }

    public class TwilioSmsSender : ISmsSender
    {
        public ServiceResult<string> SendMessage(string msg, string phoneNumber)
        {
            string phone = phoneNumber;
            if (!phoneNumber.StartsWith("+"))
            {
                phone = "+" + phoneNumber;
            }
            string AccountSid = "ACedfdae56e060c7f7e0704e695f198982";
            string AuthToken = "eb8a27031f102651dc6ddb678de2fda4";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var result = twilio.SendMessage("+12014293869", phone, msg);
            if (string.IsNullOrEmpty(result.Sid))
            {
                string errorMessage = string.Format("TWILIO ERROR. code={0}, phone={1}, errorMessage={2}", result.ErrorCode, phoneNumber, result.ErrorMessage);
                string userErrorMsg = string.Empty;
                if (result.RestException != null)
                {
                    var restEx = result.RestException;
                    errorMessage += string.Format("\r\n restCode={0}, restMsg={1}, restMoreInfo={2}, restStatus={3}",
                        restEx.Code, restEx.Message, restEx.MoreInfo, restEx.Status);
                    userErrorMsg = GetMessageByCode(restEx.Code);
                }
                if (string.IsNullOrEmpty(userErrorMsg))
                {
                    var exception = new Exception(errorMessage);
                    var signal = ErrorSignal.FromContext(HttpContext.Current);
                    signal.Raise(exception, HttpContext.Current);
                    return ServiceResult.SuccessResult<string>(result.Sid);
                }
                return ServiceResult.ErrorResult<string>(userErrorMsg);
            }
            return ServiceResult.SuccessResult<string>(result.Sid);
        }

        private string GetMessageByCode(string code)
        {
            switch (code)
            {
                case "21614":
                    return "Указанный номер не является телефонным номером. Проверьте правильность номера.";
                case "21211":
                    return "Указанный номер не действительный. Проверьте правильность номера.";
            }
            return string.Empty;
        }
    }
}