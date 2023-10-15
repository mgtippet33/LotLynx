using AutoMapper;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.WebAPI.Models;

namespace UserIdentity.WebAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserResponseModel, UserModel>().ReverseMap();
    }
}