namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Achievement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AchievementMedia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Type = c.Int(nullable: false),
                        VideoProvider = c.Int(),
                        EntityId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Step = c.Int(nullable: false),
                        Value = c.String(),
                        Status = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AchievementTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AchievementRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            CreateTable(
                "dbo.AchievementTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(),
                        Title = c.String(),
                        Values = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AchievementMedia", "EntityId", "dbo.Achievements");
            DropForeignKey("dbo.Achievements", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Achievements", "TypeId", "dbo.AchievementTypes");
            DropForeignKey("dbo.AchievementRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementRatings", "RatedEntityId", "dbo.Achievements");
            DropIndex("dbo.AchievementRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.AchievementRatings", new[] { "UserId" });
            DropIndex("dbo.Achievements", new[] { "UserId" });
            DropIndex("dbo.Achievements", new[] { "TypeId" });
            DropIndex("dbo.AchievementMedia", new[] { "EntityId" });
            DropTable("dbo.AchievementTypes");
            DropTable("dbo.AchievementRatings");
            DropTable("dbo.Achievements");
            DropTable("dbo.AchievementMedia");
        }
    }
}
