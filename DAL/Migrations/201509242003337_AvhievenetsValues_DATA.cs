namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvhievenetsValues_DATA : DbMigration
    {
        public override void Up()
        {
            Sql("set IDENTITY_INSERT AchievementTypeValues ON;");

            Sql(
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (1, 1,'/Content/images/exercise/tightening/1.png', '5');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (2, 1,'/Content/images/exercise/tightening/2.png', '12');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (3, 1,'/Content/images/exercise/tightening/3.png', '18');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (4, 1,'/Content/images/exercise/tightening/4.png', '25');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (5, 1,'/Content/images/exercise/tightening/5.png', '32');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (6, 1,'/Content/images/exercise/tightening/6.png', '38');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (7, 1,'/Content/images/exercise/tightening/7.png', '42');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (8, 1,'/Content/images/exercise/tightening/8.png', '47');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (9, 1,'/Content/images/exercise/tightening/9.png', '50');"
                );

            Sql(
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (10, 2,'/Content/images/exercise/push-up/1.png', '10');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (11, 2,'/Content/images/exercise/push-up/2.png', '18');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (12, 2,'/Content/images/exercise/push-up/3.png', '30');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (13, 2,'/Content/images/exercise/push-up/4.png', '40');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (14, 2,'/Content/images/exercise/push-up/5.png', '50');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (15, 2,'/Content/images/exercise/push-up/6.png', '65');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (16, 2,'/Content/images/exercise/push-up/7.png', '75');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (17, 2,'/Content/images/exercise/push-up/8.png', '90');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (18, 2,'/Content/images/exercise/push-up/9.png', '100');"
                );

            Sql(
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (19, 3,'/Content/images/exercise/push-up/1.png', '1');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (20, 3,'/Content/images/exercise/push-up/2.png', '3');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (21, 3,'/Content/images/exercise/push-up/3.png', '7');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (22, 3,'/Content/images/exercise/push-up/4.png', '12');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (23, 3,'/Content/images/exercise/push-up/5.png', '17');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (24, 3,'/Content/images/exercise/push-up/6.png', '22');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (25, 3,'/Content/images/exercise/push-up/7.png', '27');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (26, 3,'/Content/images/exercise/push-up/8.png', '30');"
                );

            Sql(
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (27, 4,'/Content/images/exercise/push-up/1.png', '30');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (28, 4,'/Content/images/exercise/push-up/2.png', '45');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (29, 4,'/Content/images/exercise/push-up/3.png', '60');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (30, 4,'/Content/images/exercise/push-up/4.png', '75');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (31, 4,'/Content/images/exercise/push-up/5.png', '90');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (32, 4,'/Content/images/exercise/push-up/6.png', '110');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (33, 4,'/Content/images/exercise/push-up/7.png', '140');" +
    "insert into AchievementTypeValues (Id, TypeId, CupImage, Value) values (34, 4,'/Content/images/exercise/push-up/8.png', '190');"
                );

        }

        public override void Down()
        {
            Sql("delete from AchievementTypeValues where Id between 1 and 34");
        }
    }
}
