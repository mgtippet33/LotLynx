
using Auction.Domain.Enums;

namespace Auction.Domain.Entities;

public class Lot : BaseEntity
{    
    public string AuctionId { get; set; } = null!;
          
    public string Title { get; set; } = null!;
        
    public string Description { get; set; } = null!;
    
    public string? Image { get; set; }

    public decimal StartPrice { get; set; }

    public LotStatus Status { get; set; }
}
