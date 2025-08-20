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
        builder.Property(w => w.Title).IsRequired().HasMaxLength(200);

        builder.HasOne(w => w.Aircraft)
            .WithMany(a => a.WorkOrders)
            .HasForeignKey(w => w.AircraftId);

        builder.HasMany(w => w.Tasks)
            .WithOne(t => t.WorkOrder)
            .HasForeignKey(t => t.WorkOrderId);

        builder.HasMany(w => w.Defects)
            .WithOne(d => d.WorkOrder)
            .HasForeignKey(d => d.WorkOrderId);
    }
}
