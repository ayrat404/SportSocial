namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TapeEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tape",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JournalId = c.Int(),
                        AchievemetId = c.Int(),
                        UserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.AchievemetId)
                .ForeignKey("dbo.Journals", t => t.JournalId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.JournalId)
                .Index(t => t.AchievemetId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tape", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tape", "JournalId", "dbo.Journals");
            DropForeignKey("dbo.Tape", "AchievemetId", "dbo.Achievements");
            DropIndex("dbo.Tape", new[] { "UserId" });
            DropIndex("dbo.Tape", new[] { "AchievemetId" });
            DropIndex("dbo.Tape", new[] { "JournalId" });
            DropTable("dbo.Tape");
        }
    }
}
