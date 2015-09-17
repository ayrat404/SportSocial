namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Achievement_AdditionalFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Achievements", "DurationDays", c => c.Int(nullable: false));
            AddColumn("dbo.Achievements", "Started", c => c.DateTime());
            AddColumn("dbo.Profiles", "AchievementVoiceCount", c => c.Int(nullable: false));
            DropColumn("dbo.Achievements", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Achievements", "Duration", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Profiles", "AchievementVoiceCount");
            DropColumn("dbo.Achievements", "Started");
            DropColumn("dbo.Achievements", "DurationDays");
        }
    }
}
