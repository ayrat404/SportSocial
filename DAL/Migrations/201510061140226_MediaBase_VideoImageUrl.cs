namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaBase_VideoImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AchievementMedia", "VideoImageUrl", c => c.String());
            AddColumn("dbo.JournalMedias", "VideoImageUrl", c => c.String());
            AddColumn("dbo.UserAvatarPhotoes", "VideoImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAvatarPhotoes", "VideoImageUrl");
            DropColumn("dbo.JournalMedias", "VideoImageUrl");
            DropColumn("dbo.AchievementMedia", "VideoImageUrl");
        }
    }
}
