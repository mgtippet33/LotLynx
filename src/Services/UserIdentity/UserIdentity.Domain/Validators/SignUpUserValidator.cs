using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Domain.Resources.Validators;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Models;

namespace UserIdentity.Domain.Validators;

public class SignUpUserValidator : AbstractValidator<SignUpUserModel>
{
    private readonly UserManager<User> _userManager;
    
    public SignUpUserValidator(UserManager<User> userManager)
    {
        _userManager = userManager;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(CommonResource.Email_NotEmpty);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(CommonResource.Password_NotEmpty);
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SignUpUserResource.FirstName_NotEmpty);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SignUpUserResource.LastName_NotEmpty);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SignUpUserResource.LastName_NotEmpty);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(SignUpUserResource.Email_NotCorrectFormat);
        
        RuleFor(x => x.Email)
            .MustAsync(async (x, cancellationToken) => !await DoesEmailExist(x, cancellationToken))
            .WithMessage(SignUpUserResource.Email_Exists);
        
        RuleFor(x => x.Password)
            .MinimumLength(5)
            .Must(HasValidPassword)
            .WithMessage(SignUpUserResource.Password_NotCorrectFormat);
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(SignUpUserResource.Password_NotMatch);
    }

    private async Task<bool> DoesEmailExist(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user != null;
    }
    
    private bool HasValidPassword(string password)
    {
        var lowercase = new Regex("[a-z]+");
        var uppercase = new Regex("[A-Z]+");
        var digit = new Regex("(\\d)+");
        var symbol = new Regex("(\\W)+");

        return !string.IsNullOrEmpty(password) && lowercase.IsMatch(password) && uppercase.IsMatch(password) && digit.IsMatch(password) && symbol.IsMatch(password);
    }
}