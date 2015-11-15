namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Achievement_VoteRatio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Achievements", "VotesRatio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Achievements", "VotesRatio");
        }
    }
}
