using System.Collections.Generic;

namespace Sastt.Domain;

public class Aircraft
{
    public int Id { get; set; }
    public string TailNumber { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Base { get; set; }

    public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
