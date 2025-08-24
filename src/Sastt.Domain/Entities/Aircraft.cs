using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class Aircraft : Base
{
    public string TailNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Base { get; set; } = string.Empty;
    public AircraftStatus Status { get; set; } = AircraftStatus.Active;
    public IList<Defect> Defects { get; set; } = new List<Defect>();
}
