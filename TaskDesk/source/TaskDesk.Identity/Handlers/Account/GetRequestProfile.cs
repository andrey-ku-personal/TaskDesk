namespace TaskDesk.Identity.Handlers.Account;

public class GetRequestProfile : Profile
{
    public GetRequestProfile()
    {
        CreateMap<GetRequest, GetQuery>();
    }
}