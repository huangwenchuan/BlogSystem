namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Mark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feels", "UserId", "dbo.Users");
            DropForeignKey("dbo.Feels", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Feels", new[] { "ArticleId" });
            DropIndex("dbo.Feels", new[] { "UserId" });
            DropTable("dbo.Feels");
        }
    }
}
