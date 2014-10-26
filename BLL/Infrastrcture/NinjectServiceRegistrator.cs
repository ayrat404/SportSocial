using BLL.Infrastrcture.IdentityConfig;
using BLL.Sms;
using DAL;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Extensions.Conventions;

namespace BLL.Infrastrcture
{
    public class NinjectServiceRegistrator
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind(x => x
                .FromThisAssembly()
                .Select(t => t.Name.EndsWith("Service"))// && !t.Name.StartsWith("Sms"))
                .BindDefaultInterfaces());

            kernel.Bind(x => x
                .FromAssemblyContaining(typeof(IRepository))
                .Select(t => t.Name.EndsWith("Repository"))
                .BindDefaultInterfaces());

            kernel.Bind<ISmsService>().To<SmsServiceBase>();

            kernel.Bind<EntityDbContext>().ToSelf();
            kernel.Bind<IUserStore<AppUser>>().ToMethod(ctx => new UserStore<AppUser>(ctx.Kernel.Get<EntityDbContext>()));
            kernel.Bind<AppUserManager>().ToMethod(AppUserManager.Create);
            kernel.Bind<AppRoleManager>().ToSelf();
        }
    }
}
