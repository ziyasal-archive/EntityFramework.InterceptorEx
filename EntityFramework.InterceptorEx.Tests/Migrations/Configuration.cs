using System.Data.Entity.Migrations;

namespace EntityFramework.InterceptorEx.Tests.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BloggingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BloggingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Blogs.AddOrUpdate(blog => blog.Name,
                new Blog { Name = "Blog1" },
                new Blog { Name = "Blog2" },
                new Blog { Name = "Blog3" },
                new Blog { Name = "Blog4" },
                new Blog { Name = "Blog5" }
                );
            //
        }
    }
}
