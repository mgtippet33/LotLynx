using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Contracts.Services;

public interface IAuthService
{
    Task<bool> SingInAsync(SignInUserModel model);

    Task SignUpAsync(SignUpUserModel model);
}