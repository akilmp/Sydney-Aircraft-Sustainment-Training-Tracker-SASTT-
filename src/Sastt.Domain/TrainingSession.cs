using System;
using System.Collections.Generic;

namespace Sastt.Domain;

public class TrainingSession
{
    public int Id { get; set; }
    public int PilotId { get; set; }
    public Pilot? Pilot { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
}
