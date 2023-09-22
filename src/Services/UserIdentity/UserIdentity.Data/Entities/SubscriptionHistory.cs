using UserIdentity.Data.Enums;

namespace UserIdentity.Data.Entities;

public class SubscriptionHistory
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;    

    public SubscriptionType Type { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual User User { get; set; } = new User();
}
