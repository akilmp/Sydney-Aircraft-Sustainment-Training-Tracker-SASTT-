using Microsoft.EntityFrameworkCore;
using Sastt.Application.Common.Interfaces;
using Sastt.Domain.Entities;
using WorkOrderTask = Sastt.Domain.WorkOrderTask;


namespace Sastt.Infrastructure.Persistence;

public class SasttDbContext : DbContext, IApplicationDbContext
{
    public SasttDbContext(DbContextOptions<SasttDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aircraft> Aircraft => Set<Aircraft>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
    public DbSet<Defect> Defects => Set<Defect>();
    public DbSet<Pilot> Pilots => Set<Pilot>();
    public DbSet<PilotCurrency> PilotCurrencies => Set<PilotCurrency>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SasttDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => base.SaveChangesAsync(cancellationToken);
}
