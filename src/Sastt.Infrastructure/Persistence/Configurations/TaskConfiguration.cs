using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Description).IsRequired().HasMaxLength(500);
        builder.Property(t => t.Status).HasConversion<int>();

        builder.HasOne<WorkOrder>()
               .WithMany(w => w.Tasks)
               .HasForeignKey(t => t.WorkOrderId);
    }
}
