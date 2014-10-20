using BLL.Sms;
using DAL;
using DAL.Repository.Interfaces;
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
                .Select(t => t.Name.EndsWith("Service") && !t.Name.StartsWith("Sms"))
                .BindDefaultInterfaces());

            kernel.Bind(x => x
                .FromAssemblyContaining(typeof(IRepository))
                .Select(t => t.Name.EndsWith("Repository"))
                .BindDefaultInterfaces());

            kernel.Bind<ISmsService>().To<SmsServiceBase>();

            kernel.Bind<EntityDbContext>().ToSelf();
        }
    }
}
