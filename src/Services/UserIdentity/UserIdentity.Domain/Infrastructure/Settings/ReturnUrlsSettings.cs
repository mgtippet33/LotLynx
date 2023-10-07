namespace UserIdentity.Domain.Infrastructure.Settings;

public class ReturnUrlsSettings
{
    public const string SectionName = "ReturnUrls";

    public string WebUrl { get; set; } = null!;

    public string LoginPageUrl { get; set; } = null!;
}