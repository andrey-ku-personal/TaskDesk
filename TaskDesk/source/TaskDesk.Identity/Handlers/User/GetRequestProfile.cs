using AutoMapper;

namespace TaskDesk.Identity.Handlers.User;

public class GetRequestProfile : Profile
{
    public GetRequestProfile()
    {
        CreateMap<GetRequest, GetQuery>();
    }
}