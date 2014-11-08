namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAddLikeDislikeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "DisLikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "DisLikes");
            DropColumn("dbo.Posts", "Likes");
        }
    }
}
