using System.Collections.Generic;
using Sastt.Domain.Enums;

namespace Sastt.Domain;

public class WorkOrder
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Open;

    public ICollection<WorkOrderTask> Tasks { get; set; } = new List<WorkOrderTask>();
    public ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
