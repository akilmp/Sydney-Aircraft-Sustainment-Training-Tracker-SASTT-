using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class Task : Base
{
    public Guid WorkOrderId { get; set; }
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
}
