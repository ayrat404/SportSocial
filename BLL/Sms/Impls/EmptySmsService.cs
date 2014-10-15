using System;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using BLL.Common.Objects;
using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;

namespace BLL.Sms
{
    public class EmptySmsService: ISmsService
    {
        private EntityDbContext _db;

        public EmptySmsService(EntityDbContext db)
        {
            _db = db;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }

        public bool TimeNotExpired(string userId)
        {
            var sms = _db.SmsCodes.Where(s => s.UserId == userId
                                              && s.Expired > DateTime.Now
                                             ).ToList();
            return false;
        }

        public bool CanResendCode(string userId)
        {
            throw new System.NotImplementedException();
        }

        public ServiceResult GenerateAndSendCode(string userId)
        {
            var result = new ServiceResult {Success = false};
            var sms = _db.SmsCodes.Where(s => s.UserId == userId).OrderByDescending(s => s.Created).FirstOrDefault();
            if (sms != null && sms.Expired < DateTime.Now)
            {
                if ((DateTime.Now - sms.RetryTime).Seconds <= 0)
                {
                    result.ErrorMessage = "Время для повторной отправки смс не наступило";
                    return result;
                }
                var msg = GenerateMessage(sms.Code);
                SendMessage(msg);
                sms.Modified = DateTime.Now;
                _db.SaveChanges();
                result.Success = true;
                return result;
            }
            else
            {
                string code = GenerateCode();
                var msg = GenerateMessage(code);
                SendMessage(msg);
                var smsCode = new SmsCode()
                {
                    Code = code,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Expired = DateTime.Now.AddMinutes(120),
                    RetryTime = DateTime.Now.AddSeconds(40),
                    UserId = userId,
                    Deleted = false,
                };
                _db.SmsCodes.Add(smsCode);
                _db.SaveChanges();
                result.Success = true;
                return result;
            }
        }

        public ServiceResult VerifyCode(string userId, string code)
        {
            var result = new ServiceResult {Success = false};
            var sms = _db.SmsCodes.Where(s => s.UserId == userId).OrderByDescending(s => s.Created).FirstOrDefault();
            if (sms == null)
            {
                result.ErrorMessage = "Не найдено смс";
                return result;
            }
            if (sms.Expired < DateTime.Now)
            {
                result.ErrorMessage = "Время ввода смс истекло. Повторите запрос.";
                return result;
            }
            if (sms.Code == code)
            {
                result.Success = true;
                return result;
            }
            result.ErrorMessage = "Введен неверный код.";
            return result;
        }

        public void SendMessage(string msg)
        {
            
        }

        private string GenerateCode()
        {
            int code = new Random().Next(1000, 9999);
            return code.ToString();
        }

        private string GenerateMessage(string code = null)
        {
            if (string.IsNullOrEmpty(code))
                code = new Random().Next(1000, 9999).ToString();
            var msg = string.Format("Код подтверждения: {0}", code);
            return msg;
        }
    }
}