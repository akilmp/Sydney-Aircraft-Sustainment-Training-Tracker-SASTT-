namespace Sastt.Domain.Entities;

public class PilotCurrency : Base
{
    public Guid PilotId { get; set; }
    public string CurrencyType { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public DateTime AchievedDate { get; set; }
}
