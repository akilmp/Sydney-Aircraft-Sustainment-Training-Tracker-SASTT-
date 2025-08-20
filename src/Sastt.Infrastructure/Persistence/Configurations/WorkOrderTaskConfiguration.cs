using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class WorkOrderTaskConfiguration : IEntityTypeConfiguration<WorkOrderTask>
{
    public void Configure(EntityTypeBuilder<WorkOrderTask> builder)
    {
        builder.ToTable("WorkOrderTasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Description).IsRequired().HasMaxLength(500);

        builder.HasOne(t => t.WorkOrder)
            .WithMany(w => w.Tasks)
            .HasForeignKey(t => t.WorkOrderId);
    }
}
