﻿using AutoMapper;

namespace TaskDesk.Identity.Handlers.User.Models;

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserModel, Domain.Entities.User>().ReverseMap();
    }
}