using MediatR;
using TaskDesk.Shared.Queries.Filter;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public  class GetAllRequest : FilterModel, IRequest<FilteredResult<ProjectViewModel>>
{
}
