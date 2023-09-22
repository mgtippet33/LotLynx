using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Data.Entities;

namespace UserIdentity.Data.EntityTypeConfigurations;

public class SubscriptionHistoryConfiguration : IEntityTypeConfiguration<SubscriptionHistory>
{
    public void Configure(EntityTypeBuilder<SubscriptionHistory> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ToTable("SubscriptionHistory");
    }
}
