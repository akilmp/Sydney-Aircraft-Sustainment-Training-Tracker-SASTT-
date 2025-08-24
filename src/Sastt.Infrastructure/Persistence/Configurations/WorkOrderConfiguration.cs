using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("WorkOrders");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Status).HasConversion<int>();

        builder.HasMany(w => w.Tasks)
               .WithOne(t => t.WorkOrder)
               .HasForeignKey(t => t.WorkOrderId);

        builder.HasOne(w => w.Aircraft)
               .WithMany()
               .HasForeignKey(w => w.AircraftId);
    }
}
