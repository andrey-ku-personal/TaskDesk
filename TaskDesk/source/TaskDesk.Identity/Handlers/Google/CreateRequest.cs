using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers.Google;

public class CreateRequest : BaseCreateRequest, IRequest<UserModel>
{
}