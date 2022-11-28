using FluentValidation;

namespace TaskDesk.Identity.Handlers.User;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
	public GetRequestValidator()
	{
        RuleFor(x => x.Id).GreaterThan(0).When(x => x.Id.HasValue);
        RuleFor(x => x.UserIdOrEmail).NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.UserIdOrEmail));
    }
}