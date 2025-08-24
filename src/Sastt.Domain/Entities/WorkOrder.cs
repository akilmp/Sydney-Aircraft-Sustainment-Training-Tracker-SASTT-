using Sastt.Domain.Enums;
using Sastt.Domain.Events;

namespace Sastt.Domain.Entities;

public class WorkOrder : Base
{
    public Guid AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? PlannedStart { get; set; }
    public DateTime? PlannedEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public WorkOrderStatus Status { get; private set; } = WorkOrderStatus.Draft;
    public IList<Task> Tasks { get; set; } = new List<Task>();
    public IList<Defect> Defects { get; set; } = new List<Defect>();

    public void UpdateStatus(WorkOrderStatus newStatus)
    {
        if (newStatus != Status)
        {
            Status = newStatus;
            AddDomainEvent(new WorkOrderStatusChangedEvent(Id, newStatus));
        }
    }
}
