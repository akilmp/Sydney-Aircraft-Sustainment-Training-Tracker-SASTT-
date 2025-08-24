using Microsoft.EntityFrameworkCore;
using Sastt.Domain.Entities;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Infrastructure.Persistence;

public class SasttDbContext : DbContext
{
    public SasttDbContext(DbContextOptions<SasttDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aircraft> Aircraft => Set<Aircraft>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Defect> Defects => Set<Defect>();
    public DbSet<Pilot> Pilots => Set<Pilot>();
    public DbSet<PilotCurrency> PilotCurrencies => Set<PilotCurrency>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SasttDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
