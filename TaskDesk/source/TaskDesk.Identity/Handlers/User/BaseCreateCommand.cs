using TaskDesk.Identity.Handlers.User.Models;
using MediatR;

namespace TaskDesk.Identity.Handlers.User;

public class BaseCreateCommand : UserModel, IRequest<UserModel>
{
}