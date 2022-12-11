using AutoMapper.EquivalencyExpression;
using TaskDesk.Identity.Handlers.Account.Models;

namespace TaskDesk.Identity.Handlers.Account;

public class UpdateRequestProfile : Profile
{
    public UpdateRequestProfile()
    {
        CreateMap<UpdateRequest, Domain.Entities.User>()
            .EqualityComparison((src, dest) => dest.Id == src.Id)
            .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver<UpdateRequest>>())
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateRequest, GetRequest>();
        CreateMap<UpdateRequest, GetQuery>();
    }
}
