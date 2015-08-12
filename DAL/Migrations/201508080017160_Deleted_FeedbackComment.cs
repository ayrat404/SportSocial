namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleted_FeedbackComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedbackComments", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedbackComments", "Deleted");
        }
    }
}
