namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVerificationCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealTimeVerifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Code = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        SerialNO = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealTimeVerifications", "UserId", "dbo.Users");
            DropIndex("dbo.RealTimeVerifications", new[] { "UserId" });
            DropTable("dbo.RealTimeVerifications");
        }
    }
}
