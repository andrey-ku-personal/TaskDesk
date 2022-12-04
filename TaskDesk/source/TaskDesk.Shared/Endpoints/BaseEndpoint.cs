using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskDesk.Shared.Endpoints;

[ApiController]
public class BaseEndpoint : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult> Send<TRequest, TResponse>(TRequest request)
        where TRequest : IRequest<TResponse>
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}