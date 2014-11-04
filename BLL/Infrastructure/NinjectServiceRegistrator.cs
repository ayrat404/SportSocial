using BLL.Admin.Conference;
using BLL.Admin.Conference.Impls;
using BLL.Infrastructure.IdentityConfig;
using BLL.Sms;
using BLL.Storage;
using BLL.Storage.Impls;
using DAL;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Extensions.Conventions;

namespace BLL.Infrastructure
{
    public class NinjectServiceRegistrator
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<EntityDbContext>().ToMethod(EntityDbContext.Create);
            kernel.Bind(x => x
                .FromThisAssembly()
                .Select(t => t.Name.EndsWith("Service") && !t.Name.Contains("Sms"))
                .BindDefaultInterfaces());

            kernel.Bind(x => x
                .FromAssemblyContaining(typeof(IRepository))
                .Select(t => t.Name.EndsWith("Repository"))
                .BindDefaultInterfaces());

            kernel.Bind<ISmsService>().To<SmsServiceBase>();
            kernel.Bind<IConferenceService>().To<ConferenceService>();
            kernel.Bind<IFileService>().To<FileService>();

            kernel.Bind<IUserStore<AppUser>>().ToMethod(ctx => new UserStore<AppUser>(ctx.Kernel.Get<EntityDbContext>()));
            kernel.Bind<AppUserManager>().ToMethod(AppUserManager.Create);
            kernel.Bind<AppRoleManager>().ToSelf();
        }
    }
}
