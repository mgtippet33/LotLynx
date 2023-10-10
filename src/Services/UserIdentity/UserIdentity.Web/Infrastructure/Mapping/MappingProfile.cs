using AutoMapper;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Web.Models;

namespace UserIdentity.Web.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginViewModel, SignInUserModel>().ReverseMap();
        CreateMap<RegisterViewModel, SignUpUserModel>().ReverseMap();
        CreateMap<UserProfileResponseModel, UserModel>().ReverseMap();
    }
}