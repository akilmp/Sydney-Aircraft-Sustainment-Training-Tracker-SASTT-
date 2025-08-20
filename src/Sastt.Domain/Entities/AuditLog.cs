namespace Sastt.Domain.Entities;

public class AuditLog : Base
{
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
