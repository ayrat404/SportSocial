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
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (1, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Подтягивания', '');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (2, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Отжимания от пола', '');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (3, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Приседания на одной ноге', '');" +
    "insert into AchievementTypes (Id, ImgUrl, Title, VideoUrl) values (4, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Короткие скручивания', '');"
    );
        }

        public override void Down()
        {
            Sql("delete from AchievementTypes where Id in (1, 2, 3, 4);");
        }
    }
}
