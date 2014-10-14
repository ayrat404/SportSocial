using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DomainModel
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
    }
}