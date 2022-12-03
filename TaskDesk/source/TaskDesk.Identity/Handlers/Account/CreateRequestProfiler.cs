using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers.Account;

public class CreateRequestProfiler : Profile
{
    public CreateRequestProfiler()
    {
        CreateMap<CreateRequest, Domain.Entities.User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver<CreateRequest>>())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom<UserPasswordHashResolver<CreateRequest>>())
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateRequest, GetRequest>();
    }
}