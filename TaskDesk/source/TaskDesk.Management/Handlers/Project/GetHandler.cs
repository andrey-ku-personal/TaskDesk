using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Domain;
using TaskDesk.Shared.Exceptions;
using TaskDesk.Shared.Queries;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public class GetHandler : IRequestHandler<GetRequest, ProjectModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ProjectModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Projects
            .ByQuery(_mapper.Map<GetQuery>(request))
            .ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"Project/{request.Id} was not found");
    }
}