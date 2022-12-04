using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDesk.Shared.Enums;

namespace TaskDesk.Identity.Handlers.Token;

public class TokerRequestValidator : AbstractValidator<TokenRequest>
{
	public TokerRequestValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.UserIdOrEmail).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty().When(x => x.Type == GrandType.Password);
    }
}
