namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Achievement_RatingCommentVoice : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AchievementMedia", new[] { "EntityId" });
            CreateTable(
                "dbo.AchievementComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalRating = c.Int(nullable: false),
                        Text = c.String(),
                        ByFortress = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AchievementComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.AchievementCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AchievementComments", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.AchievementVoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteFor = c.Boolean(nullable: false),
                        AchievementId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.AchievementId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AchievementId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Achievements", "TotalRating", c => c.Int(nullable: false));
            AlterColumn("dbo.AchievementMedia", "EntityId", c => c.Int());
            CreateIndex("dbo.AchievementMedia", "EntityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AchievementVoices", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementVoices", "AchievementId", "dbo.Achievements");
            DropForeignKey("dbo.AchievementComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementCommentRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementCommentRatings", "RatedEntityId", "dbo.AchievementComments");
            DropForeignKey("dbo.AchievementComments", "CommentForId", "dbo.AchievementComments");
            DropForeignKey("dbo.AchievementComments", "CommentedEntityId", "dbo.Achievements");
            DropIndex("dbo.AchievementVoices", new[] { "UserId" });
            DropIndex("dbo.AchievementVoices", new[] { "AchievementId" });
            DropIndex("dbo.AchievementCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.AchievementCommentRatings", new[] { "UserId" });
            DropIndex("dbo.AchievementComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.AchievementComments", new[] { "CommentForId" });
            DropIndex("dbo.AchievementComments", new[] { "UserId" });
            DropIndex("dbo.AchievementMedia", new[] { "EntityId" });
            AlterColumn("dbo.AchievementMedia", "EntityId", c => c.Int(nullable: false));
            DropColumn("dbo.Achievements", "TotalRating");
            DropTable("dbo.AchievementVoices");
            DropTable("dbo.AchievementCommentRatings");
            DropTable("dbo.AchievementComments");
            CreateIndex("dbo.AchievementMedia", "EntityId");
        }
    }
}
