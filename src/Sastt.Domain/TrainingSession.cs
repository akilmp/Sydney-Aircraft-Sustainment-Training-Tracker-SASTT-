using System;

namespace Sastt.Domain;

public class TrainingSession
{
    public int Id { get; set; }
    public int PilotId { get; set; }
    public Pilot? Pilot { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Result { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
