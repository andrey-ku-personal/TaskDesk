using FluentValidation;

namespace TaskDesk.Management.Handlers.Project;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
	public GetRequestValidator()
	{
		RuleFor(x => x.Id).GreaterThan(0);
	}
}
