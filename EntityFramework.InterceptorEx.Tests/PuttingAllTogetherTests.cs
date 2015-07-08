using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using Common.Testing.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFramework.InterceptorEx.Tests
{
    public class PuttingAllTogetherTests : TestBase
    {
        protected override void FinalizeSetUp()
        {
            DbInterception.Add(new WithNoLockInterceptor());
            DbInterception.Add(new WithTransactionInterceptor());
        }

        [Test]
        public void WithTransactionInterceptor_Should_Intercept_CommandText_Test()
        {
            StringBuilder builder = new StringBuilder();

            BloggingContext context = new BloggingContext();

            context.Database.Log = s => builder.Append(s);

            List<Blog> blogs = context.Blogs.Where(blog => blog.Name.StartsWith("Lo")).ToList();

            var sql = builder.ToString();

            sql.Should().NotBeNullOrEmpty();
            sql.Should().Contain("WITH (NOLOCK)");
            sql.Should().Contain("BEGIN TRAN");
        }
    }
}