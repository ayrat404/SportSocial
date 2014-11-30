using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Ninject;
using Ninject.Activation;

namespace BLL.Infrastructure.IdentityConfig
{
    public class AppUserManager: UserManager<AppUser, int>
    {
        public AppUserManager(IUserStore<AppUser, int> store) : base(store)
        {
        }

        public static AppUserManager Create(IContext context)
        {
            var userManager = new AppUserManager(context.Kernel.Get<IUserStore<AppUser, int>>());
            //userManager.SmsService = context.Kernel.Get<ISmsService>();
            return userManager;
        }
    }
}