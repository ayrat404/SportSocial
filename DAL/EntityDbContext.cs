﻿using System.Data.Entity;
using DAL.DomainModel;
using DAL.DomainModel.Achievement;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.ConferenceEntities;
using DAL.DomainModel.FeedBackEntities;
using DAL.DomainModel.JournalEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;

namespace DAL
{
    public class EntityDbContext: IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public EntityDbContext() : base("EntityDbContext")
        {
        }
        public DbSet<SmsCode> SmsCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Pay> Pays { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserAvatarPhoto> UserAvatarPhotos { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRating> PostRatings { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCommentRating> BlogCommentRatings { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceComment> ConferenceComments { get; set; }
        public DbSet<ConferenceCommentRating> ConferenceCommentRatings { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackComment> FeedbackComments { get; set; }
        public DbSet<FeedbackRating> FeedbackRatings { get; set; }
        public DbSet<FeedbackCommentRating> FeedbackCommentRatings { get; set; }
        public DbSet<FeedbackType> FeedbackTypes { get; set; }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalRating> JournalRatings { get; set; }
        public DbSet<JournalComment> JournalComments { get; set; }
        public DbSet<JournalCommentRating> JournalCommentRatings { get; set; }
        public DbSet<JournalMedia> JournalImages { get; set; }
        public DbSet<JournalTag> JournalTags { get; set; }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AchievementType> AchievementTypes { get; set; }
        public DbSet<AchievementTypeValue> AchievementTypeValues { get; set; }
        public DbSet<AchievementMedia> AchievementMedia { get; set; }
        public DbSet<AchievementRating> AchievementRatings { get; set; }
        public DbSet<Tape> Tape { get; set; }

        public DbSet<Subscribe> Subscribes { get; set; }
        
        public DbSet<Tag> Tags { get; set; }

        static EntityDbContext()
        {
            Database.SetInitializer(new DbInit());
        }

        public static EntityDbContext Create(IContext context)
        {
            return new EntityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogComment>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("BlogComments");
                });

            modelBuilder.Entity<UserAvatarPhoto>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("UserAvatarPhotoes");
                });


            //modelBuilder.Entity<>()
            //    .Map(m =>
            //    {
            //        m.MapInheritedProperties();
            //        m.ToTable("UserAvatarPhotoes");
            //    });
            //    .HasRequired(t => t.User)
            //    .WithMany()
            //    .HasForeignKey(t => t.UserId)
            //    .WillCascadeOnDelete();

            modelBuilder.Entity<BlogComment>()
                .HasOptional<BlogComment>(t => t.CommentFor)
                .WithMany()
                .HasForeignKey(comment => comment.CommentForId);

            modelBuilder.Entity<Journal>()
                .HasRequired(t => t.User)
                .WithMany(t => t.Journals)
                .HasForeignKey(j => j.UserId);

            //modelBuilder.Entity<JournalTag>()
            //    .HasRequired(t => t.Journal)
            //    .WithMany(t => t.Tags)
            //    .HasForeignKey(j => j.JournalId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<JournalComment>()
                .HasRequired(t => t.User)
                .WithMany(t => t.JournalComments)
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JournalMedia>()
                .HasOptional(t => t.Enity)
                .WithMany(t => t.Media)
                .HasForeignKey(j => j.EntityId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AchievementRating>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<AchievementMedia>()
            //    .Map(m =>
            //    {
            //        m.MapInheritedProperties();
            //        m.ToTable("AchievementMedia");
            //    });

            modelBuilder.Entity<AchievementMedia>()
                .HasOptional(t => t.Enity)
                .WithMany(t => t.AchievementMedia)
                .HasForeignKey(j => j.EntityId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AchievementVoice>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AchievementComment>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AchievementCommentRating>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JournalRating>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JournalCommentRating>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JournalMediaRating>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscribe>()
                .HasRequired(t => t.FolowerUser)
                .WithMany(t => t.Subscribes)
                .HasForeignKey(j => j.FolowerUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscribe>()
                .HasRequired(t => t.ToUser)
                .WithMany(t => t.Folowers)
                .HasForeignKey(j => j.ToUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Achievement>()
                .HasRequired(t => t.AchievementType)
                .WithMany()
                .HasForeignKey(j => j.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Achievement>()
                .HasRequired(t => t.Value)
                .WithMany()
                .HasForeignKey(j => j.ValueId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<UserAvatarPhoto>()
                .HasOptional(t => t.User)
                .WithMany(u => u.UserMedia)
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAvatarPhoto>()
                .HasOptional(t => t.Enity)
                .WithMany(u => u.UserAvatarPhotos)
                .HasForeignKey(j => j.EntityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tape>()
                .HasOptional(t => t.Achievement)
                .WithMany()
                .HasForeignKey(j => j.AchievemetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tape>()
                .HasOptional(t => t.Journal)
                .WithMany()
                .HasForeignKey(j => j.JournalId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tape>()
                .HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(j => j.UserId)
                .WillCascadeOnDelete(true);
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