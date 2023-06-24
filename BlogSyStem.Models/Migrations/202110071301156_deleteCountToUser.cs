namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteCountToUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "FansCount");
            DropColumn("dbo.Users", "FocusCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FocusCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "FansCount", c => c.Int(nullable: false));
        }
    }
}
