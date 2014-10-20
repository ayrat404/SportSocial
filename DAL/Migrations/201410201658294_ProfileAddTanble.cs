namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileAddTanble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropTable("dbo.Profiles");
        }
    }
}
