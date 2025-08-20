using Sastt.Domain.Enums;
using Sastt.Domain.Events;

namespace Sastt.Domain.Entities;

public class WorkOrder : Base
{
    public Guid AircraftId { get; set; }
    public WorkOrderStatus Status { get; private set; } = WorkOrderStatus.Open;
    public Priority Priority { get; set; } = Priority.Medium;
    public IList<Task> Tasks { get; set; } = new List<Task>();

    public void UpdateStatus(WorkOrderStatus newStatus)
    {
        if (newStatus != Status)
        {
            Status = newStatus;
            DomainEvents.Add(new WorkOrderStatusChangedEvent(Id, newStatus));
        }
    }
}
