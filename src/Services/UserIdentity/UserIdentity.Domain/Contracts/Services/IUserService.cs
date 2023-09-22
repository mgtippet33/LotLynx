namespace UserIdentity.Domain.Contracts.Services;

public interface IUserService
{
    Task GetUserProfileAsync(string id);
}
