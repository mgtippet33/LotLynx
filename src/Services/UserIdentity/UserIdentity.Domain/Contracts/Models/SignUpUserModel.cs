namespace UserIdentity.Domain.Contracts.Models;

public class SignUpUserModel : UserModel
{
    public string Password { get; set; } = null!;
    
    public string ConfirmPassword { get; set; } = null!;
}
