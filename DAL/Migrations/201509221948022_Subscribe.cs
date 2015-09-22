namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscribe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FolowerUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FolowerUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ToUserId)
                .Index(t => t.FolowerUserId)
                .Index(t => t.ToUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscribes", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscribes", "FolowerUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Subscribes", new[] { "ToUserId" });
            DropIndex("dbo.Subscribes", new[] { "FolowerUserId" });
            DropTable("dbo.Subscribes");
        }
    }
}
