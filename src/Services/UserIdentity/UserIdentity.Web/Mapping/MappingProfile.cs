using AutoMapper;
using UserIdentity.Web.Models;
using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Web.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginViewModel, SignInUserModel>().ReverseMap();
        CreateMap<RegisterViewModel, SignUpUserModel>().ReverseMap();
    }
}