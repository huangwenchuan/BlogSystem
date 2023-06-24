namespace BlogSyStem.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogSyStem.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            //AutomaticMigrationDataLossAllowed = false; //减去字段使用不能为 true
        }

        protected override void Seed(BlogSyStem.Models.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
