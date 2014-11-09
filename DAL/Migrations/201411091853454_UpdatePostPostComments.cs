namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePostPostComments : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BlogComments", name: "PostId", newName: "CommentedEntityId");
            RenameIndex(table: "dbo.BlogComments", name: "IX_PostId", newName: "IX_CommentedEntityId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BlogComments", name: "IX_CommentedEntityId", newName: "IX_PostId");
            RenameColumn(table: "dbo.BlogComments", name: "CommentedEntityId", newName: "PostId");
        }
    }
}
