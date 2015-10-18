namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUser_ExternalInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RegisterType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RegisterType");
        }
    }
}
