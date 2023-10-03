using AutoMapper;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Infrastructure.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,UserModel>().ReverseMap();
    }
}
