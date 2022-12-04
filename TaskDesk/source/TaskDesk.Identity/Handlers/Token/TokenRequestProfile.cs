using TaskDesk.Identity.Handlers.Account.Models;
using TaskDesk.Identity.Handlers.Account;

namespace TaskDesk.Identity.Handlers.Token;

public class TokenRequestProfile : Profile
{
    public TokenRequestProfile()
    {
        CreateMap<TokenRequest, GetRequest>();
    }
}