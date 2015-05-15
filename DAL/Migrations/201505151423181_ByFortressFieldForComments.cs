namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ByFortressFieldForComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "ByFortress", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.ConferenceComments", "ByFortress", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.FeedbackComments", "ByFortress", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedbackComments", "ByFortress");
            DropColumn("dbo.ConferenceComments", "ByFortress");
            DropColumn("dbo.BlogComments", "ByFortress");
        }
    }
}
