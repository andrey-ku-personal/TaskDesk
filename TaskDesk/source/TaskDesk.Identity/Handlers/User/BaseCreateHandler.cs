using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using TaskDesk.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Identity.Handlers.User.Models;

namespace TaskDesk.Identity.Handlers.User;

public class BaseCreateHandler<TRequest> : IRequestHandler<TRequest, UserModel>
    where TRequest : BaseCreateRequest
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

    public async Task<UserModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Users
                .Persist(_mapper)
                .InsertOrUpdateAsync(request, cancellationToken);

        await context.CommitTransactionAsync();

        request.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
    }
}