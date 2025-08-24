using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class TrainingSessionConfiguration : IEntityTypeConfiguration<TrainingSession>
{
    public void Configure(EntityTypeBuilder<TrainingSession> builder)
    {
        builder.ToTable("TrainingSessions");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.ScheduledFor).IsRequired();
        builder.Property(t => t.Completed).IsRequired();
        builder.Property(t => t.Location).IsRequired().HasMaxLength(200);
    }
}
