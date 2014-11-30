namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkEntities : DbMigration
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
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.BlogComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Text = c.String(),
                        RubricId = c.Int(nullable: false),
                        Lang = c.String(),
                        ImageUrl = c.String(),
                        Status = c.Byte(nullable: false),
                        CancelMessage = c.String(),
                        TotalRating = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubrics", t => t.RubricId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RubricId);
            
            CreateTable(
                "dbo.Rubrics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Lang = c.String(),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SmsCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Code = c.String(),
                        RetryTime = c.DateTime(nullable: false),
                        Expired = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BlogImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContentType = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        PayType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCount = c.Int(nullable: false),
                        PaySatus = c.Int(nullable: false),
                        Comment = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Label = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostRatings", "RatedEntityId", "dbo.Posts");
            DropForeignKey("dbo.Pays", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pays", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Conferences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogCommentRatings", "RatedEntityId", "dbo.BlogComments");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "CommentForId", "dbo.BlogComments");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SmsCodes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "RubricId", "dbo.Rubrics");
            DropForeignKey("dbo.BlogComments", "CommentedEntityId", "dbo.Posts");
            DropIndex("dbo.PostRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.PostRatings", new[] { "UserId" });
            DropIndex("dbo.Pays", new[] { "ProductId" });
            DropIndex("dbo.Pays", new[] { "UserId" });
            DropIndex("dbo.Conferences", new[] { "UserId" });
            DropIndex("dbo.SmsCodes", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.Posts", new[] { "RubricId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.BlogComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.BlogCommentRatings", new[] { "RatedEntityId" });
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.PostRatings");
            DropTable("dbo.Products");
            DropTable("dbo.Pays");
            DropTable("dbo.Conferences");
            DropTable("dbo.BlogImages");
            DropTable("dbo.SmsCodes");
            DropTable("dbo.Profiles");
            DropTable("dbo.Rubrics");
            DropTable("dbo.Posts");
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogCommentRatings");
        }
    }
}
