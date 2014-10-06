using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DomainModel
{
    public class AppRole: IdentityRole
    {
         public AppRole(): base() { }

         public AppRole(string name): base(name) { }
    }
}