using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Common.Objects;
using BLL.Sms.Impls;
using DAL;
using DAL.DomainModel;
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
            var sms = _db.SmsCodes.Where(s => s.UserId == userId).OrderByDescending(s => s.Created).FirstOrDefault();
            if (sms != null && sms.Expired > DateTime.Now && !sms.Verified)
            {
                if ((DateTime.Now - sms.RetryTime).Seconds <= 0)
                {
                    result.ErrorMessage = "Время для повторной отправки смс не наступило".Resource(this);
                    return result;
                }
                var msg = GenerateMessage(sms.Code);
                SendMessage(msg, phoneNumber, sms);
                sms.Modified = DateTime.Now;
                _db.SaveChanges();
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
                SendMessage(msg, phoneNumber, sms);
                _db.SmsCodes.Add(sms);
                _db.SaveChanges();
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

        public virtual void SendMessage(string msg, string phoneNumber, SmsCode smsCode)
        {
            ISmsSender sender;
            SmsProviderType providerType;
            if (!smsCode.Smses.Any())
            {
                providerType = SmsProviderType.SmsPilot;
            }
            else
            {
                providerType = (SmsProviderType) smsCode.Smses.OrderBy(s => s.Created).Last().SmsProvider;
            }
            sender = providerType == SmsProviderType.SmsPilot ? (ISmsSender)new TwilioSmsSender() : new SmsPilotSmsService();
            string externalId = string.Empty;
            #if !DEBUG
            externalId = sender.SendMessage(msg, phoneNumber);
            #endif
            smsCode.Smses.Add(new SmsMessage()
            {
                Message = msg,
                Phone = phoneNumber,
                ExternalId = externalId,
                SmsProvider = (int)providerType
            });
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
        string SendMessage(string msg, string phoneNumber);
    }

    public class TwilioSmsSender : ISmsSender
    {
        public string SendMessage(string msg, string phoneNumber)
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
            return result.Sid;
        }
    }
}