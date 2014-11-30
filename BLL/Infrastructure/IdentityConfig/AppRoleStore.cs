using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Infrastructure.IdentityConfig
{
    public class AppRoleStore: RoleStore<AppRole, int, AppUserRole>
    {
        public AppRoleStore(EntityDbContext context): base(context)
        {
            
        }
    }
}