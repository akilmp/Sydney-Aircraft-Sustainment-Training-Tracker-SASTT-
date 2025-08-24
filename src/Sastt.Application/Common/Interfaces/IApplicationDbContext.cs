using Microsoft.EntityFrameworkCore;
using Sastt.Domain.Entities;
using TaskEntity = Sastt.Domain.Entities.Task;

namespace Sastt.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Aircraft> Aircraft { get; }
    DbSet<WorkOrder> WorkOrders { get; }
    DbSet<TaskEntity> Tasks { get; }
    DbSet<Defect> Defects { get; }
    DbSet<Pilot> Pilots { get; }
    DbSet<TrainingSession> TrainingSessions { get; }
    DbSet<PilotCurrency> PilotCurrencies { get; }
    DbSet<User> Users { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
