
namespace Auction.Domain.Entities;

public class Bid : BaseEntity
{
    public string LotId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public decimal Amount { get; set; }
}
