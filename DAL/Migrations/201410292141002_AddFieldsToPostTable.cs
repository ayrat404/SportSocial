namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToPostTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Rubric_Id", "dbo.Rubrics");
            DropIndex("dbo.Posts", new[] { "Rubric_Id" });
            DropColumn("dbo.Posts", "RubricId");
            RenameColumn(table: "dbo.Posts", name: "Rubric_Id", newName: "RubricId");
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            AddColumn("dbo.Posts", "Status", c => c.Byte(nullable: false));
            AddColumn("dbo.Posts", "CancelMessage", c => c.String());
            AlterColumn("dbo.Posts", "RubricId", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "RubricId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "RubricId");
            AddForeignKey("dbo.Posts", "RubricId", "dbo.Rubrics", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "RubricId", "dbo.Rubrics");
            DropIndex("dbo.Posts", new[] { "RubricId" });
            AlterColumn("dbo.Posts", "RubricId", c => c.Int());
            AlterColumn("dbo.Posts", "RubricId", c => c.String());
            DropColumn("dbo.Posts", "CancelMessage");
            DropColumn("dbo.Posts", "Status");
            DropColumn("dbo.Posts", "ImageUrl");
            RenameColumn(table: "dbo.Posts", name: "RubricId", newName: "Rubric_Id");
            AddColumn("dbo.Posts", "RubricId", c => c.String());
            CreateIndex("dbo.Posts", "Rubric_Id");
            AddForeignKey("dbo.Posts", "Rubric_Id", "dbo.Rubrics", "Id");
        }
    }
}
