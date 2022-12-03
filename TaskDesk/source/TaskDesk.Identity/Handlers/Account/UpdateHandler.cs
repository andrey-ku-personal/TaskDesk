using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Domain;
using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Shared.Exceptions;
using TaskDesk.Shared.Queries;

namespace TaskDesk.Identity.Handlers.Account;

public class UpdateHandler : IRequestHandler<UpdateRequest, UserModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = context.Users
            .ByQuery(_mapper.Map<GetQuery>(request))
            .FirstOrDefault();

        if (entity == null)
            throw new NotFoundException($"User/{request.Id} was not found");

        await context.Users
           .Persist(_mapper)
           .InsertOrUpdateAsync(request, cancellationToken);

        await context.CommitTransactionAsync();

        return await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
    }
}
