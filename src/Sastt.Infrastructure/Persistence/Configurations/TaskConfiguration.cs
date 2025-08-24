using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Description).IsRequired().HasMaxLength(500);
        builder.Property(t => t.Status).HasConversion<int>();
        builder.Property(t => t.DueDate);

        builder.HasOne(t => t.WorkOrder)
               .WithMany(w => w.Tasks)
               .HasForeignKey(t => t.WorkOrderId);
    }
}
