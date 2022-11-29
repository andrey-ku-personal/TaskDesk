using AutoMapper;
using TaskDesk.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Identity.Handlers.Google;
using TaskDesk.Identity.Handlers;

namespace Desk.Identity.Handlers.Google;

public class CreateHandler : BaseCreateHandler<CreateRequest>
{
    public CreateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper) : base(contextFactory, mediator, mapper)
    {
    }
}