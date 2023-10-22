using Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AuctionEntity = Auction.Domain.Entities.Auction;

namespace Auction.Infrastructure.Persistance;

public class AuctionDbContext : DbContext
{

    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    public DbSet<AuctionEntity> Auctions { get; set; }

    public DbSet<Bid> Bids { get; set; }

    public DbSet<Lot> Lots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionDbContext).Assembly);
    }
}
