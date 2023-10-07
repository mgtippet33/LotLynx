using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace UserIdentity.Web.Models;

public class RegisterViewModel
{
    [ValidateNever]
    public string FirstName { get; set; } = null!;
    
    [ValidateNever]
    public string LastName { get; set; } = null!;
    
    [ValidateNever]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    
    [ValidateNever]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [ValidateNever]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
    
    [ValidateNever]
    public string ReturnUrl { get; set; } = null!;
}
