# enumerable-to-queryable-linq

When we write the linq code that use set operation (concat, union,intersect), sometimes we get many errors. After we search the error, usually we just change from queryable to enumerable. That solution is just fine, but we also must know the difference between enumerable and queryable. Enumerable would call database twice when we use the set operation, but when we use queryable we only call database once. This will impact the performance.

I have listed 3 common errors when we use set operation on linq. Each error has a different solution. I will explain it in an error-solution order. 

### Error

System.InvalidOperationException: Unable to translate set operation when matching columns on both sides have different store types.

### Solution 

From 

```csharp
select new 
{
  Description = x.Description
}
```
To
```csharp
select new 
{
  Description = Convert.ToString(x.Description)
}
```

You should change `varName` to `Convert.ToString(varName)` on property that has different Max Length. Don't use `varName.ToString()` method. Why? Because `Convert.ToString(varName)` will be converted to `CONVERT(NVarChar(MAX),table_name.column_name)` on SQL Server and `table_name.column_name::text` on PostgreSQL.

Code Example:
https://github.com/rahmatalhakam/enumerable-to-queryable-linq/blob/8c65c52875c9749169a1ec27effb9cdbd26962b7/Controllers/TransactionController.cs#L273-L285

More Info: https://github.com/dotnet/efcore/issues/19129

### Error

System.InvalidOperationException: Unable to translate set operation after client projection has been applied. Consider moving the set operation before the last 'Select' call.

### Solution

From 

```csharp
var a = 
( 
  from x in _dbContext.TableNameA
  select new 
  {
    Date = x.Description.ToString("dd/MM/yyyy")
  }
).AsQueryable();

var b = 
( 
  from x in _dbContext.TableNameB
  select new 
  {
    Date = x.Description.ToString("dd/MM/yyyy")
  }
).AsQueryable();

var result = a.Concat(b).ToList();
```
To
```csharp
var a = 
( 
  from x in _dbContext.TableNameA
  select new 
  {
    Date = x.DateDoc
  }
).AsQueryable();

var b = 
( 
  from x in _dbContext.TableNameB
  select new 
  {
    Date = x.DateDoc
  }
).AsQueryable();

var result = a.Concat(b)
  .Select(x=> new 
  {
    Date = x.Date.ToString("dd/MM/yyyy") 
  })
  .ToList();
```

You should check all interpolated string on Select call method on linq. Interpolated string will cause the error because interpolated string is client evaluation. Rewrite the interpolated string after union or concat method. Example of interpolated string

1. varName.ToString()
2. varName.ToString("dd/MM/yyyy")
3. varName1 + " " + varName2
4. $"{varName.Trim()} - {varName.Trim()}")

Code Example: https://github.com/rahmatalhakam/enumerable-to-queryable-linq/blob/8c65c52875c9749169a1ec27effb9cdbd26962b7/Controllers/TransactionController.cs#L288-L303

More Info: https://github.com/dotnet/efcore/issues/16243

### Error

System.InvalidOperationException: Unable to translate a collection subquery in a projection since either parent or the subquery doesn't project necessary information required to uniquely identify it and correctly generate results on the client side. This can happen when trying to correlate on keyless entity type. This can also happen for some cases of projection before 'Distinct' or some shapes of grouping key in case of 'GroupBy'. These should either contain all key properties of the entity that the operation is applied on, or only contain simple property access expressions.

### Solution

From 

```csharp
var result = a.Concat(b)
  .GroupBy(x => x.Code)
  .Select(x => new
  {
    Code = x.Key,
    ListDoc = x.ToList()
  }
  );
```
To
```csharp
var result = a.Concat(b)
  .ToList()
  .GroupBy(x => x.Code)
  .Select(x => new
  {
    Code = x.Key,
    ListDoc = x.ToList()
  }
  );
```

Check any ToList() on select call method in Linq. Add ToList() method before GroupBy or Distinct call.

Code Example: https://github.com/rahmatalhakam/enumerable-to-queryable-linq/blob/8c65c52875c9749169a1ec27effb9cdbd26962b7/Controllers/TransactionController.cs#L291-L310

More Info: https://github.com/dotnet/efcore/issues/16243
