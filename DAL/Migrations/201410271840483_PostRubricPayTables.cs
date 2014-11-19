using System.Data.Entity.Migrations.Builders;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostRubricPayTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        PayType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCount = c.Int(nullable: false),
                        PaySatus = c.Int(nullable: false),
                        Comment = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Text = c.String(),
                        RubricId = c.String(),
                        Lang = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Rubric_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubrics", t => t.Rubric_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Rubric_Id);
            
            CreateTable(
                "dbo.Rubrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Rubric_Id", "dbo.Rubrics");
            DropForeignKey("dbo.Pays", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pays", "ProductId", "dbo.Products");
            DropIndex("dbo.Posts", new[] { "Rubric_Id" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Pays", new[] { "ProductId" });
            DropIndex("dbo.Pays", new[] { "UserId" });
            DropTable("dbo.Rubrics");
            DropTable("dbo.Posts");
            DropTable("dbo.Products");
            DropTable("dbo.Pays");
        }
    }
}
