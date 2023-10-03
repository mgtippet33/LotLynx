namespace UserIdentity.Domain.Contracts.Models;

public class SignInUserModel
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
