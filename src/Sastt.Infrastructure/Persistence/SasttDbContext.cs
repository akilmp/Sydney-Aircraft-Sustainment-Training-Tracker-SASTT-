using Microsoft.EntityFrameworkCore;
using Sastt.Domain;
using Sastt.Domain.Entities;

namespace Sastt.Infrastructure.Persistence;

public class SasttDbContext : DbContext
{
    public SasttDbContext(DbContextOptions<SasttDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aircraft> Aircraft => Set<Aircraft>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkOrderTask> WorkOrderTasks => Set<WorkOrderTask>();
    public DbSet<Defect> Defects => Set<Defect>();
    public DbSet<Pilot> Pilots => Set<Pilot>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SasttDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
