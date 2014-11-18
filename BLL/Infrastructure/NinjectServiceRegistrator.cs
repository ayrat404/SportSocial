using BLL.Comments;
using BLL.Common.Services.CurrentUser;
using BLL.Common.Services.CurrentUser.Impls;
using BLL.Common.Services.Rating;
using BLL.Infrastructure.IdentityConfig;
using BLL.Sms;
using BLL.Sms.Impls;
using DAL;
using DAL.DomainModel;
using DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace BLL.Infrastructure
{
    public class NinjectServiceRegistrator
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<EntityDbContext>().ToMethod(EntityDbContext.Create).InRequestScope();

            kernel.Bind<IUserStore<AppUser>>().ToMethod(ctx => new UserStore<AppUser>(ctx.Kernel.Get<EntityDbContext>())).InRequestScope();
            kernel.Bind<AppUserManager>().ToMethod(AppUserManager.Create).InRequestScope();
            kernel.Bind<AppRoleManager>().ToSelf().InRequestScope();
            
            kernel.Bind(x => x
                .FromThisAssembly()
                .Select(t => t.Name.EndsWith("Service") && !t.Name.Contains("Sms"))
                .BindDefaultInterfaces()
                .Configure(c => c.InRequestScope()));

            kernel.Bind(x => x
                .FromAssemblyContaining(typeof(IRepository))
                .Select(t => t.Name.EndsWith("Repository"))
                .BindDefaultInterfaces()
                .Configure(c => c.InRequestScope()));

            #if DEBUG
            kernel.Bind<ISmsService>().To<SmsServiceBase>().InRequestScope();
            #endif
            #if !DEBUG
            kernel.Bind<ISmsService>().To<SmsPilotSmsService>().InRequestScope();
            #endif
            kernel.Bind<ICurrentUser>().To<CurrentUser>().InRequestScope();
            kernel.Bind(typeof(IGRatingService<,>)).To(typeof(RatingService<,>)).InRequestScope();
            kernel.Bind(typeof(ICommentService<,>)).To(typeof(CommentService<,>)).InRequestScope();

        }
    }
}
