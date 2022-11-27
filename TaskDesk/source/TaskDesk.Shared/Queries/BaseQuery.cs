using TaskDesk.Shared.Queries.Intefaces;
using System.Linq.Expressions;

namespace TaskDesk.Shared.Queries;

public class BaseQuery<TEntity> : IBaseQuery<TEntity> where TEntity : class
{
    public virtual Expression<Func<TEntity, bool>> GetExpression()
    {
        Expression<Func<TEntity, bool>> filter = uniqueEntity => true;
        return filter;
    }

    public virtual List<Expression<Func<TEntity, object>>> GetIncludes()
    {
        return new List<Expression<Func<TEntity, object>>>();
    }
}