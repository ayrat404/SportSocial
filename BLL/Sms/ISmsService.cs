using BLL.Common.Objects;
using Microsoft.AspNet.Identity;

namespace BLL.Sms
{
    public interface ISmsService : IIdentityMessageService
    {
        bool TimeNotExpired(string userId);

        bool CanResendCode(string userId);

        ServiceResult GenerateAndSendCode(string userId, string phoneNumber);

        ServiceResult VerifyCode(string userId, string code);

        void SendMessage(string msg, string phoneNumber);
    }
}