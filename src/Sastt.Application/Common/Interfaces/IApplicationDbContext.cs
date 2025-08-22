using Microsoft.EntityFrameworkCore;
using Sastt.Domain;

namespace Sastt.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Aircraft> Aircraft { get; }
    DbSet<WorkOrder> WorkOrders { get; }
    DbSet<WorkOrderTask> WorkOrderTasks { get; }
    DbSet<Defect> Defects { get; }
    DbSet<Pilot> Pilots { get; }
    DbSet<TrainingSession> TrainingSessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
