namespace Sastt.Domain;

public enum AircraftStatus
{
    Draft,
    Planned,
    InProgress,
    QaReview,
    Closed
}

public class Aircraft
{
    public string TailNumber { get; set; } = string.Empty;
    public string Base { get; set; } = string.Empty;
    public AircraftStatus Status { get; private set; } = AircraftStatus.Draft;

    public void AdvanceStatus()
    {
        if (Status == AircraftStatus.Closed)
        {
            throw new InvalidOperationException("Cannot advance closed work order");
        }

        Status++;
    }
}
