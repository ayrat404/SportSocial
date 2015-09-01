using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DomainModel
{
    public class AppUser: IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>, IAuditable, IEntity
    {
        public string Name { get; set; }

        public UserStatus Status { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ICollection<SmsCode> SmsCodes{ get; set; }

        public virtual ICollection<UserPhoto> UserPhotos { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }

        public virtual ICollection<ConferenceComment> ConferenceComments { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<UserAvatarPhoto> UserAvatarPhotos { get; set; }

        public virtual ICollection<Pay> Pays { get; set; }

        public virtual ICollection<Journal> Journals { get; set; }

        public virtual ICollection<JournalComment> JournalComments { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }
    }

    public class AppUserClaim: IdentityUserClaim<int> { }

    public class AppUserLogin: IdentityUserLogin<int> { }
}