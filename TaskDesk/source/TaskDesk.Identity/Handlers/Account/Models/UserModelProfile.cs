using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Handlers.Account.Models;

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserModel, Domain.Entities.User>()
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Domain.Entities.User, UserModel>();

        CreateMap<UserModel, GetQuery>();
    }
}