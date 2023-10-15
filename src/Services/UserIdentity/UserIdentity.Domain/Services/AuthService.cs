using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Resources.Validators;
using UserIdentity.Data.Entities;
using UserIdentity.Domain.Contracts.Services;

namespace UserIdentity.Domain.Services;

public class AuthService : BaseApplicationService, IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public AuthService(IServiceProvider serviceProvider, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper) : base(serviceProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }
    
    public async Task<SignInUserResultModel> SignInAsync(SignInUserModel model)
    {
        var validationResult = await ValidateModelAsync(model);

        if (validationResult.IsValid)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Email == model.Email);
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                validationResult.Errors.Add(new ValidationFailure(ValidatorProperty.GeneralProperty, SignInUserResource.User_NotExisted));
            }
        }

        return new SignInUserResultModel(model, validationResult);
    }
    
    public async Task<SignUpUserResultModel> SignUpAsync(SignUpUserModel model)
    {
        var validationResult = await ValidateModelAsync(model);

        if (validationResult.IsValid)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                validationResult.Errors.Add(new ValidationFailure(ValidatorProperty.GeneralProperty, CommonResource.CommonError));
            }
        }

        return new SignUpUserResultModel(model, validationResult);
    }
}
