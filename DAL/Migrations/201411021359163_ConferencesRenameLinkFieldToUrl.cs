namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConferencesRenameLinkFieldToUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conferences", "Url", c => c.String());
            DropColumn("dbo.Conferences", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conferences", "Link", c => c.String());
            DropColumn("dbo.Conferences", "Url");
        }
    }
}
