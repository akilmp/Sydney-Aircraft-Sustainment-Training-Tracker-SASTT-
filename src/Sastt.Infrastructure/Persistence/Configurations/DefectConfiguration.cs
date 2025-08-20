using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sastt.Domain;

namespace Sastt.Infrastructure.Persistence.Configurations;

public class DefectConfiguration : IEntityTypeConfiguration<Defect>
{
    public void Configure(EntityTypeBuilder<Defect> builder)
    {
        builder.ToTable("Defects");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Severity).HasMaxLength(20);
        builder.Property(d => d.Description).HasMaxLength(500);

        builder.HasOne(d => d.WorkOrder)
            .WithMany(w => w.Defects)
            .HasForeignKey(d => d.WorkOrderId);
    }
}
