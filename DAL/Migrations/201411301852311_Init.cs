namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogComments", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TotalRating = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.BlogComments", t => t.CommentForId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Rubrics", t => t.RubricId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RubricId);
            
            CreateTable(
                "dbo.PostRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ConferenceComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TotalRating = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentForId = c.Int(),
                        CommentedEntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.ConferenceComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ConferenceCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConferenceComments", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Lang = c.String(),
                        Avatar = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SmsCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Code = c.String(),
                        RetryTime = c.DateTime(nullable: false),
                        Expired = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserAvatarPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Rubrics",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Label = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pays", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pays", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BlogCommentRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogCommentRatings", "RatedEntityId", "dbo.BlogComments");
            DropForeignKey("dbo.BlogComments", "CommentForId", "dbo.BlogComments");
            DropForeignKey("dbo.Posts", "RubricId", "dbo.Rubrics");
            DropForeignKey("dbo.PostRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPhotoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SmsCodes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceCommentRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceCommentRatings", "RatedEntityId", "dbo.ConferenceComments");
            DropForeignKey("dbo.ConferenceComments", "CommentForId", "dbo.ConferenceComments");
            DropForeignKey("dbo.Conferences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConferenceComments", "CommentedEntityId", "dbo.Conferences");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostRatings", "RatedEntityId", "dbo.Posts");
            DropForeignKey("dbo.BlogComments", "CommentedEntityId", "dbo.Posts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pays", new[] { "ProductId" });
            DropIndex("dbo.Pays", new[] { "UserId" });
            DropIndex("dbo.UserPhotoes", new[] { "UserId" });
            DropIndex("dbo.UserAvatarPhotoes", new[] { "UserId" });
            DropIndex("dbo.SmsCodes", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ConferenceCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.ConferenceCommentRatings", new[] { "UserId" });
            DropIndex("dbo.Conferences", new[] { "UserId" });
            DropIndex("dbo.ConferenceComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.ConferenceComments", new[] { "CommentForId" });
            DropIndex("dbo.ConferenceComments", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PostRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.PostRatings", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "RubricId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.BlogComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.BlogCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.BlogCommentRatings", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
            DropTable("dbo.Pays");
            DropTable("dbo.BlogImages");
            DropTable("dbo.Rubrics");
            DropTable("dbo.UserPhotoes");
            DropTable("dbo.UserAvatarPhotoes");
            DropTable("dbo.SmsCodes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Profiles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ConferenceCommentRatings");
            DropTable("dbo.Conferences");
            DropTable("dbo.ConferenceComments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PostRatings");
            DropTable("dbo.Posts");
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogCommentRatings");
        }
    }
}
