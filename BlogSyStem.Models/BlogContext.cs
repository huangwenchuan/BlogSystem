using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace BlogSyStem.Models
{
    public class BlogContext : DbContext
    {

        public BlogContext()
            : base(nameOrConnectionString: "constring")
        {
            Database.SetInitializer<BlogContext>(strategy: null);

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region 默认
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            #endregion
            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Fans> Fans { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleToCategory> ArticleToCategory { get; set; }
        public DbSet<Feel> Feel { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<ReadingInfo> ReadingInfo { get; set; }
        public DbSet<RealTimeVerification> RealTimeVerification { get; set; }

    }


}