using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace TaskDesk.Identity.Handlers.Account.Models;

public class UserPasswordHashResolver<TSource> : IValueResolver<TSource, Domain.Entities.User, string?>
    where TSource : UserModel
{
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserPasswordHashResolver(IPasswordHasher<UserModel> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Resolve(TSource source, Domain.Entities.User destination, string? destMember, ResolutionContext context)
    {
        return _passwordHasher.HashPassword(source, source.Password);
    }
}

public partial class UserIdResolver<TSource> : IValueResolver<TSource, Domain.Entities.User, string>
    where TSource : UserModel
{
    private readonly Regex _pattern = ExtraSymbolRegex();

    public string Resolve(TSource source, Domain.Entities.User destination, string? destMember, ResolutionContext context)
    {
        return _pattern.Replace($"{source.FirstName}{source.LastName}", string.Empty);
    }

    [GeneratedRegex("[*'\",_&#^@+ \\-]")]
    private static partial Regex ExtraSymbolRegex();
}
