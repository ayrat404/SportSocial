namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogComments", "CommentForId", "dbo.BlogComments");
            DropIndex("dbo.BlogComments", new[] { "CommentForId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.BlogComments", "CommentForId");
            AddForeignKey("dbo.BlogComments", "CommentForId", "dbo.BlogComments", "Id");
        }
    }
}
