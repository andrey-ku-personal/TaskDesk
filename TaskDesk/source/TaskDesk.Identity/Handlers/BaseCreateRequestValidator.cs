using FluentValidation;

namespace TaskDesk.Identity.Handlers;

public class BaseCreateRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : BaseCreateRequest
{
    public BaseCreateRequestValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
    }
}