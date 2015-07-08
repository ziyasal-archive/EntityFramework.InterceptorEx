using System.Data.Entity;

namespace EntityFramework.InterceptorEx.Tests
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}