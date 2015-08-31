namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalsEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JournalComments",
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
                .ForeignKey("dbo.Journals", t => t.CommentedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.JournalComments", t => t.CommentForId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CommentForId)
                .Index(t => t.CommentedEntityId);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalRating = c.Int(nullable: false),
                        Text = c.String(),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.JournalMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Type = c.Int(nullable: false),
                        VideoProvider = c.Int(),
                        EntityId = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journals", t => t.EntityId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.JournalRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journals", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.JournalTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Journals", t => t.JournalId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.JournalId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JournalCommentRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalComments", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            AddColumn("dbo.UserAvatarPhotoes", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.UserAvatarPhotoes", "VideoProvider", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalCommentRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalCommentRatings", "RatedEntityId", "dbo.JournalComments");
            DropForeignKey("dbo.JournalComments", "CommentForId", "dbo.JournalComments");
            DropForeignKey("dbo.Journals", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.JournalTags", "JournalId", "dbo.Journals");
            DropForeignKey("dbo.JournalRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalRatings", "RatedEntityId", "dbo.Journals");
            DropForeignKey("dbo.JournalMedias", "EntityId", "dbo.Journals");
            DropForeignKey("dbo.JournalComments", "CommentedEntityId", "dbo.Journals");
            DropIndex("dbo.JournalCommentRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.JournalCommentRatings", new[] { "UserId" });
            DropIndex("dbo.JournalTags", new[] { "TagId" });
            DropIndex("dbo.JournalTags", new[] { "JournalId" });
            DropIndex("dbo.JournalRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.JournalRatings", new[] { "UserId" });
            DropIndex("dbo.JournalMedias", new[] { "EntityId" });
            DropIndex("dbo.Journals", new[] { "UserId" });
            DropIndex("dbo.JournalComments", new[] { "CommentedEntityId" });
            DropIndex("dbo.JournalComments", new[] { "CommentForId" });
            DropIndex("dbo.JournalComments", new[] { "UserId" });
            DropColumn("dbo.UserAvatarPhotoes", "VideoProvider");
            DropColumn("dbo.UserAvatarPhotoes", "Type");
            DropTable("dbo.JournalCommentRatings");
            DropTable("dbo.Tags");
            DropTable("dbo.JournalTags");
            DropTable("dbo.JournalRatings");
            DropTable("dbo.JournalMedias");
            DropTable("dbo.Journals");
            DropTable("dbo.JournalComments");
        }
    }
}
