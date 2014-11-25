namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBlogImageSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogImages", "Url", c => c.String());
            DropColumn("dbo.BlogImages", "Name");
            DropColumn("dbo.BlogImages", "ContentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogImages", "ContentType", c => c.String());
            AddColumn("dbo.BlogImages", "Name", c => c.String());
            DropColumn("dbo.BlogImages", "Url");
        }
    }
}
