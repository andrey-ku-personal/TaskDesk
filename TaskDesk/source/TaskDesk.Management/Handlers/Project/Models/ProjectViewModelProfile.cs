using AutoMapper;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Handlers.Project.Models;

public class ProjectViewModelProfile : Profile
{
    public ProjectViewModelProfile()
    {
        CreateProjection<Domain.Entities.Project, ProjectViewModel>();
    }
}