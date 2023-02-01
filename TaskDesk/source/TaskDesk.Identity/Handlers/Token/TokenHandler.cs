using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskDesk.Identity.Handlers.Account;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Identity.Options;
using TaskDesk.Shared.Enums;
using TaskDesk.Shared.Exceptions;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers.Token;

public class TokenHandler : IRequestHandler<TokenRequest, string>
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserPasswordStore<UserModel> _passwordStore;
    private readonly IPasswordHasher<UserModel> _passwordhasher;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TokenHandler(
        IOptions<JwtOptions> options,
        IUserPasswordStore<UserModel> passwordStore,
        IPasswordHasher<UserModel> passwordhasher,
        IMediator mediator,
        IMapper mapper)
    {
        _jwtOptions = options.Value;
        _passwordStore = passwordStore;
        _passwordhasher = passwordhasher;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
    {
        return request.Type switch
        {
            GrandType.Password => await GenerateByPassword(request, cancellationToken),
            GrandType.Google => await GenerateByGoogle(request, cancellationToken),
            _ => throw new BadRequestException($"Incorrect type {request.Type}")
        };
    }

    public async Task<string> GenerateByPassword(TokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);

        var passwordHash = await _passwordStore.GetPasswordHashAsync(user, cancellationToken);

        if (_passwordhasher.VerifyHashedPassword(user, passwordHash!, request.Password) == PasswordVerificationResult.Failed)
            throw new BadRequestException($"Wrong password");

        return Generate(user);
    }

    public async Task<string> GenerateByGoogle(TokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);

        return Generate(user);
    }

    public string Generate(UserModel userModel)
    {
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_jwtOptions.ExparedInMinutes);

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: new List<Claim> { new Claim("id", userModel.Id.ToString()), new Claim("userId", userModel.UserId) },
            notBefore: now,
            expires: expires,
            signingCredentials: new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
