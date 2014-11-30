using System;
using System.Data.Entity;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;

namespace DAL
{
    public class EntityDbContext: IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
    #if DEBUG 
        public EntityDbContext() : base("EntityDbContextDebug")
        {
        }
    #endif
    #if !DEBUG 
        public EntityDbContext() : base("EntityDbContextRelease")
        {
        }
    #endif
        public DbSet<SmsCode> SmsCodes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCommentRating> BlogCommentRatings { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserAvatarPhoto> UserAvatarPhotos { get; set; }
        public DbSet<ConferenceComment> ConferenceComments { get; set; }
        public DbSet<ConferenceCommentRating> ConferenceCommentRatings { get; set; }

        static EntityDbContext()
        {
            Database.SetInitializer(new DbInit());
        }

        public static EntityDbContext Create(IContext context)
        {
            return new EntityDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class DbInit: CreateDatabaseIfNotExists<EntityDbContext>
    {
        protected override void Seed(EntityDbContext context)
        {
            InitSetup(context);
            base.Seed(context);
        }

        private void InitSetup(EntityDbContext context)
        {
            //throw new System.NotImplementedException();
        }
    }
}