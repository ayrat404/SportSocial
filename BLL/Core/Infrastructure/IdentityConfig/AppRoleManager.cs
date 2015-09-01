using System;
using DAL.DomainModel;
using Microsoft.AspNet.Identity;

namespace BLL.Infrastructure.IdentityConfig
{
    public class AppRoleManager: RoleManager<AppRole, int>, IDisposable
    {
        public AppRoleManager(AppRoleStore store) : base(store)
        {
        }

        //public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        //{
        //    var db = context.Get<EntityDbContext>();
        //    var roleManeger = new AppRoleManager(new RoleStore<AppRole>(db));

        //    return roleManeger;
        //}

        //public static AppRoleManager Create(IContext context)
        //{
        //    var db = context.Kernel.Get<EntityDbContext>();
        //    var roleManeger = new AppRoleManager(new RoleStore<AppRole>(db));
        //    return roleManeger;
        //}
    }
}