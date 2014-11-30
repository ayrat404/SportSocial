namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileIsPaidField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "IsPaid");
        }
    }
}
