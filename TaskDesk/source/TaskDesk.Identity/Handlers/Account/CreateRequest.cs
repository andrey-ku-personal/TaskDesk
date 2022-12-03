using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers.Account;

public class CreateRequest : BaseCreateRequest, IRequest<UserModel>
{
}