namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalMediaRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JournalMediaRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RatingType = c.Int(nullable: false),
                        RatedEntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalMedias", t => t.RatedEntityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RatedEntityId);
            
            AddColumn("dbo.JournalMedias", "TotalRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalMediaRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalMediaRatings", "RatedEntityId", "dbo.JournalMedias");
            DropIndex("dbo.JournalMediaRatings", new[] { "RatedEntityId" });
            DropIndex("dbo.JournalMediaRatings", new[] { "UserId" });
            DropColumn("dbo.JournalMedias", "TotalRating");
            DropTable("dbo.JournalMediaRatings");
        }
    }
}
