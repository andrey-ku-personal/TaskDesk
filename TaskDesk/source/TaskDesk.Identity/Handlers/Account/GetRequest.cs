using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers.Account;

public class GetRequest : IRequest<UserModel>
{
    public int? Id { get; set; }
    public string? UserIdOrEmail { get; set; }
}
