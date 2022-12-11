using TaskDesk.Shared.Queries.Filter;

namespace TaskDesk.Core.Extensions;

public static class PaginationExtension
{
    public static FilteredResult<TEntity> PageResult<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize)
        where TEntity : class
    {
        var count = query.Count();

        if (pageSize <= 0)
        {
            pageSize = count;
        }

        if (pageNumber < 0)
        {
            pageNumber = 0;
        }

        var result = query.Skip(pageNumber * pageSize).Take(pageSize).ToList();

        return new FilteredResult<TEntity>(count, result);
    }
}