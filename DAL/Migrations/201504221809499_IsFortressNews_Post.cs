namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFortressNews_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsFortressNews", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsFortressNews");
        }
    }
}
