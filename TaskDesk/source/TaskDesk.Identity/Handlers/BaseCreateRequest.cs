using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers;

public class BaseCreateRequest : UserModel, IRequest<UserModel>
{
}