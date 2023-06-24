namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserSexnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Sex", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Sex", c => c.Int(nullable: false));
        }
    }
}
