using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using TaskDesk.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Identity.Handlers.User.Models;

namespace TaskDesk.Identity.Handlers.User;

public class BaseCreateHandler<TCommand> : IRequestHandler<TCommand, UserModel>
    where TCommand : BaseCreateCommand
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BaseCreateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<UserModel> Handle(TCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Users
                .Persist(_mapper)
                .InsertOrUpdateAsync(command, cancellationToken);

        await context.CommitTransactionAsync();

        command.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);
    }
}