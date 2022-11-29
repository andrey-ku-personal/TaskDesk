using TaskDesk.Identity.Handlers.User.Models;
using MediatR;

namespace TaskDesk.Identity.Handlers;

public class BaseCreateRequest : UserModel, IRequest<UserModel>
{
}