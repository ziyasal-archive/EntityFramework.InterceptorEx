using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using Common.Testing.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFramework.InterceptorEx.Tests
{
    public class NolockInterceptorTests : TestBase
    {
        protected override void FinalizeSetUp()
        {
            DbInterception.Add(new WithNoLockInterceptor());
        }

        [Test]
        public void NoLockInterceptor_Should_Intercept_CommandText_Test()
        {
            BloggingContext context = new BloggingContext();

            List<Blog> blogs = context.Blogs.Where(blog => blog.Name.StartsWith("Lo")).ToList();

            string sql = WithNoLockInterceptor.CommandText;

            sql.Should().Contain("WITH (NOLOCK)");

            Console.WriteLine(sql);

        }
    }
}
