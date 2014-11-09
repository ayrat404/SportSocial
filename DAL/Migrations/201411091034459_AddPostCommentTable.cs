namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostCommentTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
            CreateTable(
                "dbo.PostRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        RatedEntityId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            AddColumn("dbo.Posts", "TotalRating", c => c.Int(nullable: false));
            AddColumn("dbo.Profiles", "Avatar", c => c.String());
            AlterColumn("dbo.BlogComments", "CommentForId", c => c.Int(nullable: true));
            CreateIndex("dbo.BlogComments", "CommentForId");
            DropColumn("dbo.Posts", "Likes");
            DropColumn("dbo.Posts", "DisLikes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "DisLikes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            DropForeignKey("dbo.PostRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostRatings", "RatedEntityId", "dbo.Posts");
            DropIndex("dbo.PostRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.PostRatings", new[] { "UserId" });
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
            AlterColumn("dbo.BlogComments", "CommentForId", c => c.Int(nullable: false));
            DropColumn("dbo.Profiles", "Avatar");
            DropColumn("dbo.Posts", "TotalRating");
            DropTable("dbo.PostRatings");
            CreateIndex("dbo.BlogComments", "CommentForId");
        }
    }
}
