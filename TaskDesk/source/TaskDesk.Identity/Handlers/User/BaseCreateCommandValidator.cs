using FluentValidation;

namespace TaskDesk.Identity.Handlers.User;

public class BaseCreateCommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : BaseCreateCommand
{
    public BaseCreateCommandValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
    }
}