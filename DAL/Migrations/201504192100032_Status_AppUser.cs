namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status_AppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.Int(nullable: false, defaultValue: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Status");
        }
    }
}
