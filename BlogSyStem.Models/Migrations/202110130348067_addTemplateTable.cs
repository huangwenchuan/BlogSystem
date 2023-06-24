namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTemplateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Template",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SmallInt = c.Short(nullable: false),
                        TinyInt = c.Byte(nullable: false),
                        Content = c.String(unicode: false, storeType: "text"),
                        BigInt = c.Long(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Template");
        }
    }
}
