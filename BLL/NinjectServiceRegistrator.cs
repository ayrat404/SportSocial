using System.Threading;
using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Extensions.Conventions;

namespace BLL
{
    public class NinjectServiceRegistrator
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind(x => x
                .FromThisAssembly()
                .Select(t => t.Name.EndsWith("Service"))
                .BindDefaultInterface());
            kernel.Bind<EntityDbContext>().ToSelf();
            kernel.Bind<IUserStore<AppUser>>().ToMethod(ctx => new UserStore<AppUser>(ctx.Kernel.Get<EntityDbContext>()));
            kernel.Bind<AppUserManager>().ToSelf();
            kernel.Bind<AppRoleManager>().ToSelf();
        }
    }
}
