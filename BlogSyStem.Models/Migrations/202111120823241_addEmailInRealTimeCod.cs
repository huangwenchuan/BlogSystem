namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailInRealTimeCod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealTimeVerifications", "Email", c => c.String(maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealTimeVerifications", "Email");
        }
    }
}
