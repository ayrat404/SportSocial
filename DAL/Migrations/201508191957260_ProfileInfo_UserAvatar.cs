namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileInfo_UserAvatar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAvatarPhotoes", new[] { "UserId" });
            RenameColumn(table: "dbo.UserAvatarPhotoes", name: "UserId", newName: "EntityId");
            AddColumn("dbo.Profiles", "FirstName", c => c.String());
            AddColumn("dbo.Profiles", "LastName", c => c.String());
            AddColumn("dbo.Profiles", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Profiles", "Sex", c => c.Int(nullable: false));
            AddColumn("dbo.Profiles", "Experience", c => c.Int(nullable: false));
            AddColumn("dbo.UserAvatarPhotoes", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserAvatarPhotoes", "Modified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserAvatarPhotoes", "EntityId", c => c.Int());
            CreateIndex("dbo.UserAvatarPhotoes", "EntityId");
            AddForeignKey("dbo.UserAvatarPhotoes", "EntityId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAvatarPhotoes", "EntityId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAvatarPhotoes", new[] { "EntityId" });
            AlterColumn("dbo.UserAvatarPhotoes", "EntityId", c => c.Int(nullable: false));
            DropColumn("dbo.UserAvatarPhotoes", "Modified");
            DropColumn("dbo.UserAvatarPhotoes", "Created");
            DropColumn("dbo.Profiles", "Experience");
            DropColumn("dbo.Profiles", "Sex");
            DropColumn("dbo.Profiles", "BirthDate");
            DropColumn("dbo.Profiles", "LastName");
            DropColumn("dbo.Profiles", "FirstName");
            RenameColumn(table: "dbo.UserAvatarPhotoes", name: "EntityId", newName: "UserId");
            CreateIndex("dbo.UserAvatarPhotoes", "UserId");
            AddForeignKey("dbo.UserAvatarPhotoes", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
