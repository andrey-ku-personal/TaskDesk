using System.Linq.Expressions;
using TaskDesk.Shared.Queries;

namespace TaskDesk.Identity.Handlers.Account;

public class GetQuery : BaseQuery<Domain.Entities.User>
{
    public int? Id { get; set; }
    public string? UserIdOrEmail { get; set; }

    public override Expression<Func<Domain.Entities.User, bool>> GetExpression()
    {
        return filter => Id != null ? filter.Id == Id : filter.UserId == UserIdOrEmail || filter.Email == UserIdOrEmail;
    }
}