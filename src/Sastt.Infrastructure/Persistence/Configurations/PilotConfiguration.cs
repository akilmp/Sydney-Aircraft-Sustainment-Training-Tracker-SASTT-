using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class PilotConfiguration : IEntityTypeConfiguration<Pilot>
{
    public void Configure(EntityTypeBuilder<Pilot> builder)
    {
        builder.ToTable("Pilots");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

        builder.HasMany(p => p.TrainingSessions)
               .WithOne()
               .HasForeignKey(ts => ts.PilotId);
    }
}
