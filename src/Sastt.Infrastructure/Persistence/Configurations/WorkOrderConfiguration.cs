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
        builder.Property(w => w.Status).HasConversion<int>();
        builder.Property(w => w.Priority).HasConversion<int>();

        builder.HasMany(w => w.Tasks)
               .WithOne()
               .HasForeignKey(t => t.WorkOrderId);

        builder.HasOne<Aircraft>()
               .WithMany()
               .HasForeignKey(w => w.AircraftId);
    }
}
