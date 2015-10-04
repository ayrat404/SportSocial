namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile_PayInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Profiles", "LastPaymentDate", c => c.DateTime());
            AddColumn("dbo.Profiles", "LastPaidDaysCount", c => c.Int());
            AddColumn("dbo.Profiles", "IsTrial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "IsTrial");
            DropColumn("dbo.Profiles", "LastPaidDaysCount");
            DropColumn("dbo.Profiles", "LastPaymentDate");
            DropColumn("dbo.Products", "Count");
        }
    }
}
