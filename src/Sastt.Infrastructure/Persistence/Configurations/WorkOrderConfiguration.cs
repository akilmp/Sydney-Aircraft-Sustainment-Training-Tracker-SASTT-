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
        builder.Property(w => w.Description).HasMaxLength(1000);
        builder.Property(w => w.Priority).HasConversion<int>();
        builder.Property(w => w.Status).HasConversion<int>();

        builder.HasMany(w => w.Tasks)
               .WithOne(t => t.WorkOrder)
               .HasForeignKey(t => t.WorkOrderId);

        builder.HasMany(w => w.Defects)
               .WithOne(d => d.WorkOrder)
               .HasForeignKey(d => d.WorkOrderId);

        builder.HasOne(w => w.Aircraft)
               .WithMany()
               .HasForeignKey(w => w.AircraftId);
    }
}
