namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AchievementValues : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Achievements", "TypeId", "dbo.AchievementTypes");
            CreateTable(
                "dbo.AchievementTypeValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.Int(nullable: false),
                        CupImage = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AchievementTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            AddColumn("dbo.Achievements", "ValueId", c => c.Int(nullable: false));
            AddColumn("dbo.AchievementTypes", "VideoUrl", c => c.String());
            CreateIndex("dbo.Achievements", "ValueId");
            AddForeignKey("dbo.Achievements", "ValueId", "dbo.AchievementTypeValues", "Id");
            AddForeignKey("dbo.Achievements", "TypeId", "dbo.AchievementTypes", "Id");
            DropColumn("dbo.Achievements", "Value");
            DropColumn("dbo.AchievementTypes", "Values");

    //        Sql(
    //"insert into AchievementTypes values (1, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Подтягивания', '');" +
    //"insert into AchievementTypes values (2, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Отжимания от пола', '');" +
    //"insert into AchievementTypes values (3, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Приседания на одной ноге', '');" +
    //"insert into AchievementTypes values (4, '/Content/socialApp/images/groups/achievements/achievement-horizontal.png', 'Короткие скручивания', '');"
    //);


        }

        public override void Down()
        {
            AddColumn("dbo.AchievementTypes", "Values", c => c.String());
            AddColumn("dbo.Achievements", "Value", c => c.String());
            DropForeignKey("dbo.Achievements", "TypeId", "dbo.AchievementTypes");
            DropForeignKey("dbo.Achievements", "ValueId", "dbo.AchievementTypeValues");
            DropForeignKey("dbo.AchievementTypeValues", "TypeId", "dbo.AchievementTypes");
            DropIndex("dbo.AchievementTypeValues", new[] { "TypeId" });
            DropIndex("dbo.Achievements", new[] { "ValueId" });
            DropColumn("dbo.AchievementTypes", "VideoUrl");
            DropColumn("dbo.Achievements", "ValueId");
            DropTable("dbo.AchievementTypeValues");
            AddForeignKey("dbo.Achievements", "TypeId", "dbo.AchievementTypes", "Id", cascadeDelete: true);
        }
    }
}
