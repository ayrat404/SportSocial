using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Achievement;
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

        public virtual ICollection<Achievement.Achievement> Achievements { get; set; }

        public virtual ICollection<JournalComment> JournalComments { get; set; }

        public virtual ICollection<Subscribe> Folowers { get; set; }
        public virtual ICollection<Subscribe> Subscribes { get; set; }
        public virtual ICollection<JournalMedia> JournalMedia { get; set; }
        public virtual ICollection<UserAvatarPhoto> UserMedia { get; set; }
            
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        public int RegisterType { get; set; }

        public string FullName()
        {
            return Profile.FirstName + " " + Profile.LastName;
        }
    }

    public class AppUserClaim: IdentityUserClaim<int> { }

    public class AppUserLogin: IdentityUserLogin<int> { }
}