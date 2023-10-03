using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;

namespace UserIdentity.WebApi.Infrastructure.Web;

public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
{
    private const string FailedUserAuthenticationErrorName = "user_authentication_failed";

    private const string FailedUserAuthenticationErrorMessage = "The username or password is incorrect.";

    private readonly IAuthService _authService;


    public AuthorizationServerProvider(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
        context.Validated();
    }

    //public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
    //{
    //    return base.GrantAuthorizationCode(context);
    //}

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
        var signInModel = new SignInUserModel(context.UserName, context.Password);

        if (!await _authService.SingInAsync(signInModel))
        {
            context.Rejected();
            context.SetError(FailedUserAuthenticationErrorName, FailedUserAuthenticationErrorMessage);
            return;
        }

        var claim = new ClaimsIdentity(context.Options.AuthenticationType);
        claim.AddClaim(new Claim("username", context.UserName));

        context.Validated(claim);
    }
}
