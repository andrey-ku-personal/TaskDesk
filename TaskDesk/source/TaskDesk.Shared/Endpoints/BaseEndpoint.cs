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

    public async Task<ActionResult> Send<TCommand, TResponse>(TCommand command)
        where TCommand : IRequest<TResponse>
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}