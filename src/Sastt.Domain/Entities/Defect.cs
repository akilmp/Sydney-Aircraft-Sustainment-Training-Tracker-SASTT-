using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class Defect : Base
{
    public Guid AircraftId { get; set; }
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public bool IsResolved { get; set; }
}
