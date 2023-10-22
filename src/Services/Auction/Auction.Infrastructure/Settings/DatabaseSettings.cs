namespace Auction.Infrastructure.Settings;

public class DatabaseSettings
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = null!;
}
