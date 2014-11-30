using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DomainModel
{
    public class AppRole: IdentityRole<int, AppUserRole>
    {
        public AppRole(): base() { }

        public AppRole(string name)
        {
            Name = name;
        }
    }

    public class AppUserRole : IdentityUserRole<int> { }
}