using FluentValidation;

namespace TaskDesk.Identity.Handlers.Account;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);

        RuleFor(x => x.Description).MaximumLength(2048).When(x => !string.IsNullOrWhiteSpace(x.Description));
        RuleFor(x => x.Website).MaximumLength(1024).When(x => !string.IsNullOrWhiteSpace(x.Website));
    }
}
