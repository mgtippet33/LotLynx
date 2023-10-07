using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Contracts.Services;

public interface IAuthService
{
    Task<SignInUserResultModel> SignInAsync(SignInUserModel model);

    Task<SignUpUserResultModel> SignUpAsync(SignUpUserModel model);
}