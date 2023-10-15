using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Contracts.Services;

public interface IUserService
{
    Task<UserModel> GetUserProfileAsync(string id);
}
