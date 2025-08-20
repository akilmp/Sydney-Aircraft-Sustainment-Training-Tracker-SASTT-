using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class Aircraft : Base
{
    public string TailNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public IList<Defect> Defects { get; set; } = new List<Defect>();
}
