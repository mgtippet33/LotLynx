namespace UserIdentity.Domain.Infrastructure.Settings;

public class DatabaseSettings
{
    public const string SectionName = "Database";

    public string ConnectionString { get; private set; } = null!;
}
