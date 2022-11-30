using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSwag.Annotations;
using TaskDesk.Identity.Handlers.Account;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Shared.Endpoints;

namespace TaskDesk.Manager.API.Controllers;

public class AccountController : BaseEndpoint
{

    public AccountController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Account/Create")]
    [OpenApiOperation(
        operationId: "Account.Create",
        summary: "Create user",
        description: "Create user")
    ]
    [OpenApiTags("Account")]
    public async Task<ActionResult> Create([FromBody] CreateRequest request) => await Send<CreateRequest, UserModel>(request);

    [HttpPost("Account/Update")]
    [OpenApiOperation(
       operationId: "Account.Create",
       summary: "Update user",
       description: "Update user")
    ]
    [OpenApiTags("Account")]
    public async Task<ActionResult> Update([FromBody] UpdateRequest request) => await Send<UpdateRequest, UserModel>(request);

    [HttpPost("Account/Get")]
    [OpenApiOperation(
       operationId: "Account.Get",
       summary: "Get user",
       description: "Get user")
    ]
    [OpenApiTags("Account")]
    public async Task<ActionResult> Get([FromBody] GetRequest request) => await Send<GetRequest, UserModel>(request);
}
