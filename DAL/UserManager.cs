using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Ninject;
using Ninject.Activation;

namespace DAL
{
    public class AppUserManager: UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        //public static AppUserManager Create(IContext context)
        //{
        //    var db = context.Kernel.Get<EntityDbContext>();
        //    var userManager = new AppUserManager(new UserStore<AppUser>(db));
        //    return userManager;
        //}
    }
}