using BLL.Common.Services.CurrentUser;
using BLL.Common.Services.CurrentUser.Impls;
using BLL.Infrastructure.IdentityConfig;
using BLL.Sms;
using BLL.Sms.Impls;
using DAL;
using DAL.DomainModel;
using DAL.Repository;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace BLL.Infrastructure
{
    public class NinjectServiceRegistrator
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<EntityDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IUserStore<AppUser, int>>().ToMethod(ctx => new AppUserStore(ctx.Kernel.Get<EntityDbContext>())).InRequestScope();
            kernel.Bind<AppUserManager>().ToMethod(AppUserManager.Create).InRequestScope();
            kernel.Bind<AppRoleManager>().ToSelf().InRequestScope();
            
            kernel.Bind(x => x
                .FromThisAssembly()
                .Select(t => t.Name.EndsWith("Service") && !t.Name.Contains("Sms"))
                .BindDefaultInterfaces()
                .Configure(c => c.InRequestScope()));


            kernel.Bind<IRepository>().To<Repository>().InRequestScope();
            kernel.Bind<IJournalRepository>().To<JournalRepository>().InRequestScope();
            kernel.Bind<IAchievementRepository>().To<AchievementRepository>().InRequestScope();

            //kernel.Bind(x => x
            //    .FromAssemblyContaining(typeof(IRepository))
            //    .Select(t => t.Name.EndsWith("Repository"))
            //    .BindDefaultInterfaces()
            //    .Configure(c => c.InRequestScope()));

            #if DEBUG
            kernel.Bind<ISmsService>().To<SmsServiceBase>().InRequestScope();
            #endif
            #if !DEBUG
            kernel.Bind<ISmsService>().To<SmsPilotSmsService>().InRequestScope();
            #endif
            kernel.Bind<ICurrentUser>().To<CurrentUser>().InRequestScope();
        }
    }
}
