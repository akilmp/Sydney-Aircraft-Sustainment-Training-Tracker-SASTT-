using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("WorkOrders");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Title).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Priority).HasConversion<int>();
        builder.Property(w => w.PlannedStart);
        builder.Property(w => w.PlannedEnd);
        builder.Property(w => w.ActualStart);
        builder.Property(w => w.ActualEnd);
        builder.Property(w => w.Status).HasConversion<int>();

        builder.HasMany(w => w.Tasks)
               .WithOne()
               .HasForeignKey(t => t.WorkOrderId);

        builder.HasOne<Aircraft>()
               .WithMany()
               .HasForeignKey(w => w.AircraftId);
    }
}
