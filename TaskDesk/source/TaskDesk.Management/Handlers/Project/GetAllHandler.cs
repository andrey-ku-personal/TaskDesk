using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskDesk.Domain;
using TaskDesk.Management.Handlers.Project.Models;
using TaskDesk.Shared.Queries;
using TaskDesk.SharedModel.Filter;

namespace TaskDesk.Management.Handlers.Project;

public class GetAllHandler : IRequestHandler<GetAllRequest, FilteredResult<ProjectViewModel>>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetAllHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<FilteredResult<ProjectViewModel>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Projects
            .AsNoTracking()
            .ByQuery(_mapper.Map<GetAllQuery>(request))
            .ProjectTo<ProjectViewModel>(_mapper.ConfigurationProvider)
            .PaginateAsync(request);
    }
}