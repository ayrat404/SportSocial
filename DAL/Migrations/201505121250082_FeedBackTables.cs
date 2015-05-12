namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedBackTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeedbackComments", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.FeedbackComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Text = c.String(),
                        UserId = c.Int(nullable: false),
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        TotalRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feedbacks", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.FeedbackComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        Title = c.String(nullable: true),
                        FeedbackTypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        TotalRating = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeedbackTypes", t => t.FeedbackTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.FeedbackTypeId);
            
            CreateTable(
                "dbo.FeedbackTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedbackRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feedbacks", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedbackCommentRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedbackComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedbackCommentRatings", "RatedEntityId", "dbo.FeedbackComments");
            DropForeignKey("dbo.FeedbackComments", "CommentForId", "dbo.FeedbackComments");
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedbackRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedbackRatings", "RatedEntityId", "dbo.Feedbacks");
            DropForeignKey("dbo.Feedbacks", "FeedbackTypeId", "dbo.FeedbackTypes");
            DropForeignKey("dbo.FeedbackComments", "CommentedEntityId", "dbo.Feedbacks");
            DropIndex("dbo.FeedbackRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.FeedbackRatings", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "FeedbackTypeId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropIndex("dbo.FeedbackComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.FeedbackComments", new[] { "CommentForId" });
            DropIndex("dbo.FeedbackComments", new[] { "UserId" });
            DropIndex("dbo.FeedbackCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.FeedbackCommentRatings", new[] { "UserId" });
            DropTable("dbo.FeedbackRatings");
            DropTable("dbo.FeedbackTypes");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.FeedbackComments");
            DropTable("dbo.FeedbackCommentRatings");
        }
    }
}
