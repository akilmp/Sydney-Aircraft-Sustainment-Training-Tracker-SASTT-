using System;
using System.Threading;
using System.Threading.Tasks;
using Sastt.Application;
using Sastt.Domain.Entities;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Infrastructure.Services;

public class AuditLogger : IAuditLogger
{
    private readonly SasttDbContext _context;

    public AuditLogger(SasttDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(Guid userId, string action, CancellationToken cancellationToken = default)
    {
        var log = new AuditLog
        {
            UserId = userId,
            Action = action,
            Timestamp = DateTime.UtcNow
        };

        _context.Set<AuditLog>().Add(log);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

