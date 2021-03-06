namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsIsFortressField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsFortress", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsFortress");
        }
    }
}
