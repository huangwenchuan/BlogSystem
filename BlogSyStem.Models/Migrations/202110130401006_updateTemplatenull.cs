namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTemplatenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Template", "SmallInt", c => c.Short());
            AlterColumn("dbo.Template", "TinyInt", c => c.Byte());
            AlterColumn("dbo.Template", "BigInt", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Template", "BigInt", c => c.Long(nullable: false));
            AlterColumn("dbo.Template", "TinyInt", c => c.Byte(nullable: false));
            AlterColumn("dbo.Template", "SmallInt", c => c.Short(nullable: false));
        }
    }
}
