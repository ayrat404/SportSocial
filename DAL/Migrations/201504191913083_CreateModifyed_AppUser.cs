namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModifyed_AppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Created",
                c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "SYSDATETIME()"));
            AddColumn("dbo.AspNetUsers", "Modified", 
                c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "SYSDATETIME()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Modified");
            DropColumn("dbo.AspNetUsers", "Created");
        }
    }
}
