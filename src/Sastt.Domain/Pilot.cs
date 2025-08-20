using System.Collections.Generic;

namespace Sastt.Domain;

public class Pilot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}
