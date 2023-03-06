using MediatR;
using TaskDesk.Shared.Queries.Filter;
using TaskDesk.SharedModel.Enums;
using TaskDesk.SharedModel.Filter;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public  class GetAllRequest : FilterModel, IRequest<FilteredResult<ProjectViewModel>>
{
    public ProjectFilter Filter { get; set; } = default!;
}

public class ProjectFilter
{
    public List<ProjectStatus>? StatusIds { get; set; }
}
