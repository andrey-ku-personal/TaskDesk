using FluentValidation;

namespace TaskDesk.Management.Handlers.Project;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
	public CreateRequestValidator()
	{
		RuleFor(x => x.Id).Equal(0);
		RuleFor(x => x.Name).NotNull().NotEmpty();
	}
}
