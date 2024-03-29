﻿using BLL.Common.Services.CurrentUser;
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
                .Select(t => (t.Name.EndsWith("Service") || t.Name.EndsWith("MediaWorker")))
                .BindDefaultInterfaces()
                .Configure(c => c.InRequestScope()));

            kernel.Bind<IRepository>().To<Repository>().InRequestScope();
            kernel.Bind<IJournalRepository>().To<JournalRepository>().InRequestScope();
            kernel.Bind<IAchievementRepository>().To<AchievementRepository>().InRequestScope();
            kernel.Bind<IPaymentRepository>().To<PaymentRepository>().InRequestScope();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>().InRequestScope();


            kernel.Bind<ICurrentUser>().To<CurrentUser>().InRequestScope();
        }
    }
}
