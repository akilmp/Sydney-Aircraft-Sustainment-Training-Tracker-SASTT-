using System.Collections.Generic;

namespace Sastt.Domain;

public class WorkOrder
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

    public ICollection<WorkOrderTask> Tasks { get; set; } = new List<WorkOrderTask>();
    public ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
