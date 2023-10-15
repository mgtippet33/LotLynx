using AutoMapper;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SignUpUserModel, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<User, SignUpUserModel>();
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<User, SignInUserModel>().ReverseMap();
    }
}
