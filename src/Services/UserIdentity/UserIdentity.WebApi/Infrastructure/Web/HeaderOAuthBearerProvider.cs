using Microsoft.Owin.Security.OAuth;

namespace UserIdentity.WebApi.Infrastructure.Web;

public class HeaderOAuthBearerProvider : OAuthBearerAuthenticationProvider
{
    private readonly string name;

    public HeaderOAuthBearerProvider(string name)
    {
        this.name = name;
    }

    public override Task RequestToken(OAuthRequestTokenContext context)
    {
        var value = context.Request.Headers.Get(this.name);

        if (!string.IsNullOrEmpty(value))
        {
            context.Token = value;
        }

        return Task.FromResult<object>(null);
    }
}
