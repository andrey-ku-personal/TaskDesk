using MediatR;
using TaskDesk.Management.Handlers.Project.Models;
using TaskDesk.Shared.Queries.Filter;
using TaskDesk.SharedModel.Enums;
using TaskDesk.SharedModel.Filter;

namespace TaskDesk.Management.Handlers.Project;

public  class GetAllRequest : FilterModel, IRequest<FilteredResult<ProjectViewModel>>
{
    public ProjectFilter? Filter { get; set; }
}

public class ProjectFilter
{
    public List<ProjectStatus>? StatusIds { get; set; }
}
