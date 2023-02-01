using System.Linq.Expressions;
using TaskDesk.Shared.Queries;

namespace TaskDesk.Management.Handlers.Project;

public class GetQuery : BaseQuery<Domain.Entities.Project>
{
    public int? Id { get; set; }

    public override Expression<Func<Domain.Entities.Project, bool>> GetExpression() 
        => filter => filter.Id == Id;
}