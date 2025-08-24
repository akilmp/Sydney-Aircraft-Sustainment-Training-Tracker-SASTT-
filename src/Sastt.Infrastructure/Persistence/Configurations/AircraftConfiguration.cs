using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.ToTable("Aircraft");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.TailNumber).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Model).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Base).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Status).HasConversion<int>();

        builder.HasMany(a => a.Defects)
               .WithOne()
               .HasForeignKey(d => d.AircraftId);
    }
}
