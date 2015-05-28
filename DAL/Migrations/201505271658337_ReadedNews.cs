namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadedNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "ReadedNews", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "ReadedNews");
        }
    }
}
