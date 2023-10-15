using Auction.Domain.Enums;

namespace Auction.Domain.Entities;

public class Auction : BaseEntity
{
    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartTime { get; set; } 

    public DateTime EndTime { get; set; }

    public AuctionType Type { get; set; }
}
