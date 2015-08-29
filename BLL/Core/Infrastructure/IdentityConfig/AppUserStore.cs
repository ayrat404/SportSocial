using DAL;
using DAL.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Infrastructure.IdentityConfig
{
    public class AppUserStore: UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(EntityDbContext context): base(context)
        {
            
        }
    }
}