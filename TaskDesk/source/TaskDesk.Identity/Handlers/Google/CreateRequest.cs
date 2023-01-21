using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers.Google;

public class CreateRequest : BaseCreateRequest, IRequest<UserModel>
{
}