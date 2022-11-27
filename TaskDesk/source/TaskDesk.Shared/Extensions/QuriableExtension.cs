using System.Linq.Expressions;

namespace TaskDesk.Shared.Extensions;

public static class QuriableExtension
{
    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName, bool isAsceting)
    {
        return isAsceting
            ? source.OrderBy(ToLambda<TEntity>(propertyName))
            : source.OrderByDescending(ToLambda<TEntity>(propertyName));
    }

    private static Expression<Func<TEntity, object>> ToLambda<TEntity>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TEntity));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<TEntity, object>>(propAsObject, parameter);
    }
}