namespace UserIdentity.Domain.Contracts.Services;

public interface IAuthService
{
    Task SingInAsync();

    Task SignUpAsync();
}
