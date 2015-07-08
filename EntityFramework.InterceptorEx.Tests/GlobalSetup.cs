using System.Data.Entity;
using EntityFramework.InterceptorEx.Tests.Migrations;
using NUnit.Framework;

namespace EntityFramework.InterceptorEx.Tests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void ShowSomeTrace()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BloggingContext, Configuration>()); 
        }
    }
}