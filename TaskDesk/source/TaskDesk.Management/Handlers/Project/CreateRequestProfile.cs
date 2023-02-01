using AutoMapper;

namespace TaskDesk.Management.Handlers.Project;

public class CreateRequestProfile : Profile
{
    public CreateRequestProfile()
    {
        CreateMap<CreateRequest, Domain.Entities.Project>();
        CreateMap<CreateRequest, GetRequest>();
    }
}