using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace EntityFramework.InterceptorEx
{
    public class WithTransactionInterceptor : DbCommandInterceptor
    {
        private static string Wrapped_Query_Tpl = @"
                            DECLARE @v{1} INT

                            BEGIN TRAN
                               
                                {0}

                                SELECT @v{1} = @@ERROR
                                IF (@v{1} <> 0) GOTO ERR_{2}
                            COMMIT TRAN

                            ERR_{2}:
                                IF (@v{1} <> 0) BEGIN
                                    ROLLBACK TRAN
                            END";

        [ThreadStatic]
        public static bool Suppress;

        [ThreadStatic]
        public static string CommandText;

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (!Suppress)
            {
                var rnd = Guid.NewGuid().ToString("N");
                command.CommandText = String.Format(Wrapped_Query_Tpl, command.CommandText, rnd, rnd);
                CommandText = command.CommandText;
            }
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (!Suppress)
            {

                if (!command.CommandText.TrimStart().StartsWith("DECLARE @v"))
                {
                    var rnd = Guid.NewGuid().ToString("N");
                    command.CommandText = String.Format(Wrapped_Query_Tpl, command.CommandText, rnd, rnd);
                    CommandText = command.CommandText;
                }
            }
        }
    }
}