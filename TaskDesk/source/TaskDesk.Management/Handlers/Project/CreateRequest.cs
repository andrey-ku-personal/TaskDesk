using MediatR;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public class CreateRequest : ProjectModel, IRequest<ProjectModel>
{
}
