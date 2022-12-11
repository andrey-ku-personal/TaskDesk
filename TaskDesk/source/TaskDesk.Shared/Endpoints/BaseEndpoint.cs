using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskDesk.Shared.Endpoints;

[ApiController]
public class BaseEndpoint : ControllerBase
{
    public IMediator Mediator { get; init; }

    public BaseEndpoint(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task<ActionResult> Send<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest<TResponse>
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }
}