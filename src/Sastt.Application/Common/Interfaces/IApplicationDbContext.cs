using Microsoft.EntityFrameworkCore;
using Sastt.Domain;
using Entities = Sastt.Domain.Entities;

namespace Sastt.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Aircraft> Aircraft { get; }
    DbSet<Entities.WorkOrder> WorkOrders { get; }
    DbSet<Entities.WorkOrderTask> WorkOrderTasks { get; }
    DbSet<Entities.Defect> Defects { get; }
    DbSet<Pilot> Pilots { get; }
    DbSet<TrainingSession> TrainingSessions { get; }
    DbSet<PilotCurrency> PilotCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
