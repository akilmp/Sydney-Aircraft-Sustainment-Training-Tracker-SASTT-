using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class PilotCurrencyConfiguration : IEntityTypeConfiguration<PilotCurrency>
{
    public void Configure(EntityTypeBuilder<PilotCurrency> builder)
    {
        builder.ToTable("PilotCurrencies");
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.CurrencyType).IsRequired().HasMaxLength(100);
        builder.Property(pc => pc.ExpirationDate).IsRequired();

        builder.HasOne<Pilot>()
               .WithMany()
               .HasForeignKey(pc => pc.PilotId);
    }
}
