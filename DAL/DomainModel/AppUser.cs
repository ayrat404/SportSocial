using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DomainModel
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }

        public virtual ICollection<SmsCode> SmsCodes{ get; set; }

        public Profile Profile { get; set; }
    }
}