using TaskDesk.Shared.Queries;
using System.Linq.Expressions;

namespace TaskDesk.Identity.Handlers.User;

public class GetQuery : BaseQuery<Domain.Entities.User>
{
    public int? Id { get; set; }
    public string? UserIdOrEmail { get; set; }

    public override Expression<Func<Domain.Entities.User, bool>> GetExpression()
    {
        return filter => Id != null ? filter.Id == Id : filter.UserId == UserIdOrEmail || filter.Email == UserIdOrEmail;
    }
}