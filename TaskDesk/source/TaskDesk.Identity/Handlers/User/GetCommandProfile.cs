using AutoMapper;

namespace TaskDesk.Identity.Handlers.User;

public class GetCommandProfile : Profile
{
    public GetCommandProfile()
    {
        CreateMap<GetCommand, GetQuery>();
    }
}