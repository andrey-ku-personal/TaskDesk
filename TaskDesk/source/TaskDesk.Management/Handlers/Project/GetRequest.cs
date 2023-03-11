using MediatR;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Handlers.Project;

public class GetRequest : IRequest<ProjectModel>
{
    public int Id { get; set; }
}
