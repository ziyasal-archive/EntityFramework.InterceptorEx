using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using Common.Testing.NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFramework.InterceptorEx.Tests
{
    public class WithTransactionInterceptorTests : TestBase
    {
        protected override void FinalizeSetUp()
        {
            DbInterception.Add(new WithTransactionInterceptor());
        }

        [Test]
        public void WithTransactionInterceptor_Should_Intercept_CommandText_Test()
        {
            BloggingContext context = new BloggingContext();

            List<Blog> blogs = context.Blogs.Where(blog => blog.Name.StartsWith("Lo")).ToList();

            string sql = WithTransactionInterceptor.CommandText;

            sql.Should().Contain("BEGIN TRAN");

            Console.WriteLine(sql);

        }
    }
}
