using AutoMapper;

namespace TaskDesk.Management.Handlers.Project;

public class GetAllRequestProfile : Profile
{
    public GetAllRequestProfile()
    {
        CreateMap<GetAllRequest, GetAllQuery>();
    }
}