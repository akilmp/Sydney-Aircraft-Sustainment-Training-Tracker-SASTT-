using Microsoft.EntityFrameworkCore;
using Sastt.Domain;
using Sastt.Domain.Entities;

namespace Sastt.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Aircraft> Aircraft { get; }
    DbSet<WorkOrder> WorkOrders { get; }
    DbSet<WorkOrderTask> WorkOrderTasks { get; }
    DbSet<Defect> Defects { get; }
    DbSet<Pilot> Pilots { get; }
    DbSet<TrainingSession> TrainingSessions { get; }
    DbSet<PilotCurrency> PilotCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
