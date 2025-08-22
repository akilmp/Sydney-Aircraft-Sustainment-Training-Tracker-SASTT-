using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sastt.Application;

public interface IAuditLogger
{
    Task LogAsync(Guid userId, string action, CancellationToken cancellationToken = default);
}

