using AutoMapper;

namespace TaskDesk.Management.Handlers.Project;

public class GetQueryProfile : Profile
{
    public GetQueryProfile()
    {
        CreateMap<GetRequest, GetQuery>();
    }
}