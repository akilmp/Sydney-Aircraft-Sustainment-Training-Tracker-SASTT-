using MediatR;
using Sastt.Application.Pilots;
using Sastt.Domain.Events;

namespace Sastt.Application.TrainingSessions.EventHandlers;

/// <summary>
/// Handles training session completion events and updates pilot currency accordingly.
/// </summary>
public class TrainingSessionCompletedEventHandler : INotificationHandler<TrainingSessionCompletedEvent>
{
    private readonly PilotCurrencyService _service;

    public TrainingSessionCompletedEventHandler(PilotCurrencyService service)
    {
        _service = service;
    }

    public async Task Handle(TrainingSessionCompletedEvent notification, CancellationToken cancellationToken)
    {
        await _service.UpdateCurrencyAsync(notification.PilotId, DateTime.UtcNow, cancellationToken);
    }
}

