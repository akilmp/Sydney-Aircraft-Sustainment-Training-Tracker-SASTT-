using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class TrainingSessionConfiguration : IEntityTypeConfiguration<TrainingSession>
{
    public void Configure(EntityTypeBuilder<TrainingSession> builder)
    {
        builder.ToTable("TrainingSessions");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Date).IsRequired();

        builder.HasOne(t => t.Pilot)
            .WithMany(p => p.TrainingSessions)
            .HasForeignKey(t => t.PilotId);
    }
}
