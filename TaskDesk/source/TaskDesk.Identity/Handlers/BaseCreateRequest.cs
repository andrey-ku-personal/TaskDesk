using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers;

public class BaseCreateRequest : UserModel, IRequest<UserModel>
{
}