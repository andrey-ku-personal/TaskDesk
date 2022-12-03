using TaskDesk.Domain;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Identity.Handlers;
using TaskDesk.Identity.Handlers.Account;

namespace Desk.Identity.Handlers.Account;

public class CreateHandler : BaseCreateHandler<CreateRequest>
{
    public CreateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper) : base(contextFactory, mediator, mapper)
    {
    }
}