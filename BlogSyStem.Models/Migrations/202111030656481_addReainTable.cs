namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReainTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReadingInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        SerialNO = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadingInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReadingInfoes", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ReadingInfoes", new[] { "ArticleId" });
            DropIndex("dbo.ReadingInfoes", new[] { "UserId" });
            DropTable("dbo.ReadingInfoes");
        }
    }
}
