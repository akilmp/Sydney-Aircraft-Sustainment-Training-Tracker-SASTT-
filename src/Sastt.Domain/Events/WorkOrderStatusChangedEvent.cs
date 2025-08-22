using Sastt.Domain.Enums;

namespace Sastt.Domain.Events;

public record WorkOrderStatusChangedEvent(Guid WorkOrderId, WorkOrderStatus NewStatus) : IDomainEvent;
