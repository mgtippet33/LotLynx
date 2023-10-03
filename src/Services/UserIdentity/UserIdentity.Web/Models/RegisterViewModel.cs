using System.ComponentModel.DataAnnotations;

namespace UserIdentity.Web.Models;

public class RegisterViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    public string ReturnUrl { get; set; } = null!;
}
