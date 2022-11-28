using AutoMapper;
using TaskDesk.Domain;
using TaskDesk.Identity.Handlers.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Identity.Handlers.Google;

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