using FluentValidation;
using System.Text.RegularExpressions;

namespace TaskDesk.Identity.Handlers.Account;

public partial class CreateRequestValidator : BaseCreateRequestValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Password).Must(ValidPassword).MaximumLength(256);
    }

    private bool ValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 12)
            return false;

        if (!password.Any(x => char.IsUpper(x)))
            return false;

        if (!password.Any(x => char.IsLower(x)))
            return false;

        if (!password.Any(x => char.IsLetter(x)))
            return false;

        if (!password.Any(x => char.IsNumber(x)))
            return false;

        if (MyRegex().IsMatch(password))
            return false;

        return true;
    }

    [GeneratedRegex("^[a-zA-Z0-9 ]*$")]
    private static partial Regex MyRegex();
}