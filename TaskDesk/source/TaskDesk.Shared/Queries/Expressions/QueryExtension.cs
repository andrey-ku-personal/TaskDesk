using Microsoft.EntityFrameworkCore;
using TaskDesk.Core.Extensions;
using TaskDesk.Shared.Extensions;
using TaskDesk.Shared.Queries.Filter;
using TaskDesk.Shared.Queries.Intefaces;

namespace TaskDesk.Shared.Queries;

public static class QuriableExtension
{
    public static IQueryable<TEntity> ByQuery<TEntity>(this IQueryable<TEntity> items, IBaseQuery<TEntity> query)
        where TEntity : class
    {
        var result = items.Where(query.GetExpression());
        result = query.GetIncludes().Aggregate(result, (current, include) => current.Include(include));
        return result;
    }

    public static IOrderedQueryable<TEntity> ByQuery<TEntity>(this IQueryable<TEntity> items, IBaseSortQuery<TEntity> query)
        where TEntity : class
    {
        var result = items.Where(query.GetExpression());
        result = query.GetIncludes().Aggregate(result, (current, include) => current.Include(include));
        return result.OrderBy(query.GetSortingExpression());
    }

    public static FilteredResult<TEntity> Paginate<TEntity>(this IQueryable<TEntity> items, IPageFilter filter)
        where TEntity : class
    {
        return items.PageResult(filter.PageNumber, filter.PageSize);
    }
}