namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mark1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Sex");
        }
    }
}
