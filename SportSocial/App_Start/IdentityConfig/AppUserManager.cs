using BLL.Sms;
using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Ninject;
using Ninject.Activation;

namespace SportSocial.IdentityConfig
{
    public class AppUserManager: UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static AppUserManager Create(IContext context)
        {
            var userManager = new AppUserManager(context.Kernel.Get<IUserStore<AppUser>>());
            userManager.SmsService = context.Kernel.Get<ISmsService>();
            return userManager;
        }
    }
}