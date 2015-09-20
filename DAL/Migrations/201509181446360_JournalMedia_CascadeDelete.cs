namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalMedia_CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalMedias", "EntityId", "dbo.Journals");
            DropForeignKey("dbo.JournalMediaRatings", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.JournalMedias", "EntityId", "dbo.Journals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JournalMediaRatings", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalMediaRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalMedias", "EntityId", "dbo.Journals");
            AddForeignKey("dbo.JournalMediaRatings", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JournalMedias", "EntityId", "dbo.Journals", "Id");
        }
    }
}
