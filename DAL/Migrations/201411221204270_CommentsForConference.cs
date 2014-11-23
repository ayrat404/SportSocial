namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsForConference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConferenceComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TotalRating = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.ConferenceComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.UserAvatarPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ConferenceCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConferenceComments", t => t.RatedEntityId, cascadeDelete: true)
                .Index(t => t.RatedEntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConferenceCommentRatings", "RatedEntityId", "dbo.ConferenceComments");
            DropForeignKey("dbo.UserPhotoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceComments", "CommentForId", "dbo.ConferenceComments");
            DropForeignKey("dbo.ConferenceComments", "CommentedEntityId", "dbo.Conferences");
            DropIndex("dbo.ConferenceCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.UserPhotoes", new[] { "UserId" });
            DropIndex("dbo.UserAvatarPhotoes", new[] { "UserId" });
            DropIndex("dbo.ConferenceComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.ConferenceComments", new[] { "CommentForId" });
            DropIndex("dbo.ConferenceComments", new[] { "UserId" });
            DropTable("dbo.ConferenceCommentRatings");
            DropTable("dbo.UserPhotoes");
            DropTable("dbo.UserAvatarPhotoes");
            DropTable("dbo.ConferenceComments");
        }
    }
}
