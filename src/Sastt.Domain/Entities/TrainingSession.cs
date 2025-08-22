using Sastt.Domain.Events;

namespace Sastt.Domain.Entities;

public class TrainingSession : Base
{
    public Guid PilotId { get; set; }
    public DateTime ScheduledFor { get; set; }
    public bool Completed { get; private set; }

    public void MarkCompleted()
    {
        if (!Completed)
        {
            Completed = true;
            AddDomainEvent(new TrainingSessionCompletedEvent(Id, PilotId));
        }
    }
}
