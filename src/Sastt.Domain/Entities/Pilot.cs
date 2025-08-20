namespace Sastt.Domain.Entities;

public class Pilot : Base
{
    public string Name { get; set; } = string.Empty;
    public IList<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}
