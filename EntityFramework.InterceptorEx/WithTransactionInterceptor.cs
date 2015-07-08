using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace EntityFramework.InterceptorEx
{
    public class WithTransactionInterceptor : DbCommandInterceptor
    {
        private static string Wrapped_Query_Tpl = @"
                            DECLARE @p{1} INT

                            BEGIN TRAN
                               
                                {0}

                                SELECT @p{1} = @@ERROR
                                IF (@p{1} <> 0) GOTO ERR_HANDLE_BLOCK
                            COMMIT TRAN

                            ERR_HANDLE_BLOCK:
                                IF (@p{1} <> 0) BEGIN
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
                command.CommandText = String.Format(Wrapped_Query_Tpl, command.CommandText, GetTimestamp(DateTime.Now));
                CommandText = command.CommandText;
            }
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (!Suppress)
            {
                command.CommandText = String.Format(Wrapped_Query_Tpl, command.CommandText);
                CommandText = command.CommandText;
            }
        }

        private static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}