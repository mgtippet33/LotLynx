using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Resources.Validators;

namespace UserIdentity.Domain.Validators;

public class SignInUserValidator : AbstractValidator<SignInUserModel>
{
    private readonly UserManager<User> _userManager;
    
    public SignInUserValidator(UserManager<User> userManager)
    {
        _userManager = userManager;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(CommonResource.Email_NotEmpty);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(CommonResource.Password_NotEmpty);

        RuleFor(x => x)
            .NotNull()
            .MustAsync(IsUserExistedAsync)
            .WithMessage(SignInUserResource.User_NotExisted)
            .OverridePropertyName(ValidatorProperty.GeneralProperty);
    }

    private async Task<bool> IsUserExistedAsync(SignInUserModel model, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email, cancellationToken);

        return user != null && !string.IsNullOrEmpty(model.Password) && await _userManager.CheckPasswordAsync(user, model.Password);
    }
}