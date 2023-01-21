using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers.Account;

public class CreateRequest : BaseCreateRequest, IRequest<UserModel>
{
}