using FluentValidation;
using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers;

public class BaseCreateRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : UserModel
{
    public BaseCreateRequestValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
    }
}