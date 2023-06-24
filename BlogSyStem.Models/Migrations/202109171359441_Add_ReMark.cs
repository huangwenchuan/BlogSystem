namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReMark : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String());
        }
    }
}
