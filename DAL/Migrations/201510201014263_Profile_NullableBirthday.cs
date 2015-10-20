namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile_NullableBirthday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profiles", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profiles", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
