using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Domain;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public class CreateHandler : IRequestHandler<CreateRequest, ProjectModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ProjectModel> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Projects
                .Persist(_mapper)
                .InsertOrUpdateAsync(request, cancellationToken);

        await context.CommitTransactionAsync();

        request.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetRequest>(request), cancellationToken);
    }
}