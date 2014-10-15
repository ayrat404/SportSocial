using System.Security.Cryptography.X509Certificates;
using BLL.Common.Objects;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;

namespace BLL.Sms
{
    public interface ISmsService : IIdentityMessageService
    {
        bool TimeNotExpired(string userId);

        bool CanResendCode(string userId);

        ServiceResult GenerateAndSendCode(string userId);

        ServiceResult VerifyCode(string userId, string code);
    }
}