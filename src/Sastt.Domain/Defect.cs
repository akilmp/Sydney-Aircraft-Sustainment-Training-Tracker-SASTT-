using System.Collections.Generic;

namespace Sastt.Domain;

public class Defect
{
    public int Id { get; set; }
    public int WorkOrderId { get; set; }
    public WorkOrder? WorkOrder { get; set; }
    public string? Severity { get; set; }
    public string? Description { get; set; }
    public bool IsClosed { get; set; }
}
