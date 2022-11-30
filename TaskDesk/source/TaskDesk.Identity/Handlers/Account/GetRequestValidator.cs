using FluentValidation;

namespace TaskDesk.Identity.Handlers.Account;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
	public GetRequestValidator()
	{
        RuleFor(x => x.Id).NotNull().GreaterThan(0).When(x => string.IsNullOrEmpty(x.UserIdOrEmail));
        RuleFor(x => x.UserIdOrEmail).NotNull().NotEmpty().When(x => !x.Id.HasValue);
    }
}