using AutoMapper;
using TaskDesk.Identity.Handlers.User;
using TaskDesk.Identity.Handlers.User.Models;

namespace TaskDesk.Identity.Handlers.Google;

public class CreateCommandProfiler : Profile
{
    public CreateCommandProfiler()
    {
        CreateMap<CreateCommand, Domain.Entities.User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserNameResolver<CreateCommand>>())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => "$2a$10$"))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateCommand, GetCommand>();
    }
}