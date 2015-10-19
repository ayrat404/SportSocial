namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AchievementsTypes_IDENTITYINSERT : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT AchievementTypes ON;");
            Sql(
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (1, '/Content/socialApp/images/groups/achievements/pull-ups.png', 'Подтягивания', 'https://www.youtube.com/watch?v=XPsNqKDNq9M');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (2, '/Content/socialApp/images/groups/achievements/pushups.png', 'Отжимания от пола', 'https://www.youtube.com/watch?v=FKV1F7IuNpM');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (3, '/Content/socialApp/images/groups/achievements/squats.png', 'Приседания на одной ноге', 'https://www.youtube.com/watch?v=Mc_s3YWpQOo');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (4, '/Content/socialApp/images/groups/achievements/situps.png', 'Короткие скручивания', 'https://www.youtube.com/watch?v=Gj1pPx2-JZk');"
    );
        }

        public override void Down()
        {
            Sql("delete from AchievementTypes where Id in (1, 2, 3, 4);");
        }
    }
}
