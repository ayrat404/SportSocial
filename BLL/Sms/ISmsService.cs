using BLL.Common.Objects;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;

namespace BLL.Sms
{
    public interface ISmsService : IIdentityMessageService
    {
        bool TimeNotExpired(int userId);

        bool CanResendCode(int userId);

        ServiceResult GenerateAndSendCode(int userId, string phoneNumber);

        ServiceResult VerifyCode(int userId, string code);

        void SendMessage(string msg, string phoneNumber, SmsCode smsCode);
    }
}