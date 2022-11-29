using AutoMapper;
using TaskDesk.Identity.Handlers.User;
using TaskDesk.Identity.Handlers.User.Models;

namespace TaskDesk.Identity.Handlers.Google;

public class CreateRequestProfiler : Profile
{
    public CreateRequestProfiler()
    {
        CreateMap<CreateRequest, Domain.Entities.User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver<CreateRequest>>())
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => "$2a$10$"))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateRequest, GetRequest>();
    }
}