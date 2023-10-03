using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;

namespace UserIdentity.Domain.Services;

public class AuthService : IAuthService
{
    public async Task SignUpAsync(SignUpUserModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SingInAsync(SignInUserModel model)
    {
        return true; 
    }
}
