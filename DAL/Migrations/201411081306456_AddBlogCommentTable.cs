namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogCommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        RatedEntityId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogComments", t => t.RatedEntityId, cascadeDelete: true)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TotalRating = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CommentForId = c.Int(nullable: true),
                        PostId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogComments", t => t.CommentForId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCommentRatings", "RatedEntityId", "dbo.BlogComments");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.BlogComments", "CommentForId", "dbo.BlogComments");
            DropIndex("dbo.BlogComments", new[] { "PostId" });
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.BlogCommentRatings", new[] { "RatedEntityId" });
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogCommentRatings");
        }
    }
}
