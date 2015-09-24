namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Media_UserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AchievementMedia", "UserId", c => c.Int());
            AddColumn("dbo.JournalMedias", "UserId", c => c.Int());
            AddColumn("dbo.UserAvatarPhotoes", "UserId", c => c.Int());
            CreateIndex("dbo.AchievementMedia", "UserId");
            CreateIndex("dbo.JournalMedias", "UserId");
            CreateIndex("dbo.UserAvatarPhotoes", "UserId");
            AddForeignKey("dbo.JournalMedias", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AchievementMedia", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AchievementMedia", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalMedias", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAvatarPhotoes", new[] { "UserId" });
            DropIndex("dbo.JournalMedias", new[] { "UserId" });
            DropIndex("dbo.AchievementMedia", new[] { "UserId" });
            DropColumn("dbo.UserAvatarPhotoes", "UserId");
            DropColumn("dbo.JournalMedias", "UserId");
            DropColumn("dbo.AchievementMedia", "UserId");
        }
    }
}
