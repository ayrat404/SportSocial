namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmsCodeAddTable : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmsCodes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SmsCodes", new[] { "UserId" });
            DropTable("dbo.SmsCodes");
        }
    }
}
