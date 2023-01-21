using AutoMapper;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Handlers.Project.Models;

public class ProjectModelProfile : Profile
{
    public ProjectModelProfile()
    {
        CreateMap<ProjectModel, Domain.Entities.User>()
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateProjection<Domain.Entities.Project, ProjectModel>();

        CreateMap<ProjectModel, GetQuery>();
    }
}