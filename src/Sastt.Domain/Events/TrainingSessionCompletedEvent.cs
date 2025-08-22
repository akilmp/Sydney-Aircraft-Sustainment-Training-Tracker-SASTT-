namespace Sastt.Domain.Events;

public record TrainingSessionCompletedEvent(Guid TrainingSessionId, Guid PilotId) : IDomainEvent;
