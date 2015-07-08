using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;

namespace EntityFramework.InterceptorEx
{
    public class WithNoLockInterceptor : DbCommandInterceptor
    {
        private static readonly Regex TableAliasRegex = new Regex(@"(?<tableAlias>AS \[Extent\d+\](?! WITH \(NOLOCK\)))", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        [ThreadStatic]
        public static bool Suppress;

        [ThreadStatic]
        public static string CommandText;

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (!Suppress)
            {
                command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
                CommandText = command.CommandText;
            }
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (!Suppress)
            {
                command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
                CommandText = command.CommandText;
            }
        }
    }
}
