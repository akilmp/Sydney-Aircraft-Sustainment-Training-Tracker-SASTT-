using Microsoft.AspNetCore.Mvc;
using Sastt.Application;
using System;
using System.Security.Claims;
using System.Threading;

namespace Sastt.Web.Controllers;

public abstract class AuditableController : Controller
{
    private readonly IAuditLogger _auditLogger;

    protected AuditableController(IAuditLogger auditLogger)
    {
        _auditLogger = auditLogger;
    }

    protected Task AuditAsync(string action, CancellationToken cancellationToken = default)
    {
        var userIdClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid.TryParse(userIdClaim, out var userId);
        return _auditLogger.LogAsync(userId, action, cancellationToken);
    }
}

