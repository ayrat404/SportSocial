namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmsMssage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmsCodeId = c.Int(nullable: false),
                        Message = c.String(),
                        Phone = c.String(),
                        SmsProvider = c.Int(nullable: false),
                        ExternalId = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SmsCodes", t => t.SmsCodeId, cascadeDelete: true)
                .Index(t => t.SmsCodeId);
            
            AddColumn("dbo.SmsCodes", "Verified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmsMessages", "SmsCodeId", "dbo.SmsCodes");
            DropIndex("dbo.SmsMessages", new[] { "SmsCodeId" });
            DropColumn("dbo.SmsCodes", "Verified");
            DropTable("dbo.SmsMessages");
        }
    }
}
