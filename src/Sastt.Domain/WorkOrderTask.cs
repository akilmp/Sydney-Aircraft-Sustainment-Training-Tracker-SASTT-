using System.Collections.Generic;

namespace Sastt.Domain;

public class WorkOrderTask
{
    public int Id { get; set; }
    public int WorkOrderId { get; set; }
    public WorkOrder? WorkOrder { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
