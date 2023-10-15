using AutoMapper;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.WebAPI.Models;

namespace UserIdentity.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserResponseModel, UserModel>().ReverseMap();
    }
}