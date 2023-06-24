namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEnumSex : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Feels", "FeelType", c => c.Int());
            
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.Feels", "FeelType");
            
        }
    }
}
