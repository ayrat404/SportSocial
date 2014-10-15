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
                .BindDefaultInterfaces());
            kernel.Bind<EntityDbContext>().ToSelf();
        }
    }
}
