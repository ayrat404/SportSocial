namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConferencesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conferences", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Conferences", new[] { "UserId" });
            DropTable("dbo.Conferences");
        }
    }
}
