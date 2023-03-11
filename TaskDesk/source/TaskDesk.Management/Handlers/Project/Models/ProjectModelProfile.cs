using AutoMapper;
using TaskDesk.SharedModel.Handlers.Project.Models;

namespace TaskDesk.Management.Handlers.Project.Models;

public class ProjectModelProfile : Profile
{
    public ProjectModelProfile()
    {
        CreateMap<ProjectModel, Domain.Entities.Project>();
        CreateMap<ProjectModel, GetQuery>();

        CreateProjection<Domain.Entities.Project, ProjectModel>();
    }
}