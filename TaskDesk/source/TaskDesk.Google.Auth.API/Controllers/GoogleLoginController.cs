using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskDesk.Shared.Endpoints;
using MediatR;
using NSwag.Annotations;
using TaskDesk.Shared.Extensions;
using TaskDesk.Identity.Handlers.Account;
using Microsoft.Extensions.Options;
using TaskDesk.Identity.Handlers.Google;
using TaskDesk.Shared.Exceptions;

namespace TaskDesk.Auth.Google.API.Controllers;

public class LoginController : BaseEndpoint
{
    private readonly TaskDesk.Identity.Options.GoogleOptions _googleOptions;

    public LoginController(
        IMediator mediator,
        IOptions<TaskDesk.Identity.Options.GoogleOptions> options) : base(mediator)
    {
        _googleOptions = options.Value;
    }

    [HttpGet("Token/Google")]
    [OpenApiOperation(
        operationId: "Token.Challenge.Google",
        summary: "Challenge Google",
        description: "Challenge Google")
    ]
    [OpenApiTags("Token")]
    public IActionResult Index()
    {
        return new ChallengeResult(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = Url.Action("ChallengeGoogle", "Login")
        });
    }

    [HttpGet("Token/Google/Challenge")]
    [OpenApiOperation(
        operationId: "Google.Challenge",
        summary: "Google Challenge",
        description: "Google Challenge")
    ]
    [OpenApiTags("Google")]
    public async Task<IActionResult> ChallengeGoogle()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("External");

        if (!authenticateResult.Succeeded 
            || authenticateResult.Principal.Identities.IsEmpty() 
            || !authenticateResult.Principal.Identities.Any(x => x.AuthenticationType!.ToLower() == "google")
            || authenticateResult.Principal is null)
        {
            return Redirect(_googleOptions.FailedLink);
        }    

        var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)!.Value;

        try
        {
            await _mediator.Send(new GetRequest { UserIdOrEmail = email });
        }
        catch (NotFoundException)
        {
            await _mediator.Send(new Identity.Handlers.Google.CreateRequest()
            {
                Email = email,
                FirstName = authenticateResult.Principal.FindFirst(ClaimTypes.GivenName)!.Value,
                LastName = authenticateResult.Principal.FindFirst(ClaimTypes.Surname)!.Value
            });
        }

        return Redirect(_googleOptions.SuccessLink);
    }
}