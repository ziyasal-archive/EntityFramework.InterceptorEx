# EntityFramework.InterceptorEx

[![Build status](https://ci.appveyor.com/api/projects/status/6qpi6het0ve74m0t/branch/master?svg=true)](https://ci.appveyor.com/project/ziyasal/entityframework-interceptorex/branch/master)

:warning::warning::warning: _It requires more tests_

**INTRODUCTION**
- [WithNoLockInterceptor](#nolock-interceptor)
- [WithTransactionInterceptor](#transaction-interceptor)

## NoLock Interceptor

To Register
```csharp
DbInterception.Add(new WithNoLockInterceptor());
```

To Disable
```csharp
WithNoLockInterceptor.Suppress = true;
```
To Debug Sql Query
```csharp
string sql = WithNoLockInterceptor.CommandText
```

Sample Sql Query Output
```sql
SELECT [Extent1].[BlogId] AS [BlogId], [Extent1].[Name] AS [Name]  FROM [dbo].[Blogs] AS [Extent1] WITH (NOLOCK)  WHERE [Extent1].[Name] LIKE N'Lo%'
```

## Transaction Interceptor

To Register
```csharp
DbInterception.Add(new WithTransactionInterceptor());
```

To Disable
```csharp
WithTransactionInterceptor.Suppress = true;
```
Sample Sql Query Output
```sql
DECLARE @errorCode INT

BEGIN TRAN

SELECT [Extent1].[BlogId] AS [BlogId], [Extent1].[Name] AS [Name]  FROM [dbo].[Blogs] AS [Extent1] WITH (NOLOCK)  WHERE [Extent1].[Name] LIKE N'Lo%'

SELECT
	@errorCode = @@ERROR
    IF (@errorCode <> 0) GOTO ERR_HANDLE_BLOCK
    COMMIT TRAN

      ERR_HANDLE_BLOCK:    IF (@errorCode <> 0) BEGIN
        ROLLBACK TRAN
END
```

##Bugs
If you encounter a bug, performance issue, or malfunction, please add an [Issue](https://github.com/ziyasal/EntityFramework.InterceptorEx/issues) with steps on how to reproduce the problem.

##TODO
- Add more tests
- Add more documentation

##License
Code and documentation are available according to the *MIT* License (see [LICENSE](https://github.com/ziyasal/EntityFramework.InterceptorEx/blob/master/LICENSE)).
