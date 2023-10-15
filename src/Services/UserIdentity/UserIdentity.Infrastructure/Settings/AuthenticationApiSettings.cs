namespace UserIdentity.Infrastructure.Settings;

public class AuthenticationApiSettings
{
    public const string SectionName = "Authentication";

    public string Authority { get; set; } = null!;
    
    public string Audience { get; set; } = null!;
}