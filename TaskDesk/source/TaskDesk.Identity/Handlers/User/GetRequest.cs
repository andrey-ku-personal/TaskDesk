using TaskDesk.Identity.Handlers.User.Models;
using MediatR;

namespace TaskDesk.Identity.Handlers.User;

public class GetRequest : IRequest<UserModel>
{
    public int? Id { get; set; }
    public string? UserIdOrEmail { get; set; }
}
