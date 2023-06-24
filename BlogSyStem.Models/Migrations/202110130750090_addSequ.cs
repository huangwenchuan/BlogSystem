namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSequ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ArticleToCategories", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Categories", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Comments", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Fans", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Feels", "SerialNO", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Template", "SerialNO", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Template", "SerialNO");
            DropColumn("dbo.Feels", "SerialNO");
            DropColumn("dbo.Fans", "SerialNO");
            DropColumn("dbo.Comments", "SerialNO");
            DropColumn("dbo.Categories", "SerialNO");
            DropColumn("dbo.ArticleToCategories", "SerialNO");
            DropColumn("dbo.Users", "SerialNO");
            DropColumn("dbo.Articles", "SerialNO");
        }
    }
}
