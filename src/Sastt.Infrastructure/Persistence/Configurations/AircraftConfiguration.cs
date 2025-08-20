using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.ToTable("Aircraft");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.TailNumber).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Type).HasMaxLength(50);
        builder.Property(a => a.Base).HasMaxLength(10);

        builder.HasMany(a => a.WorkOrders)
            .WithOne(w => w.Aircraft)
            .HasForeignKey(w => w.AircraftId);
    }
}
