namespace The_Engneering.Contracts.Bill;

public class BillResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 4);
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string PayType { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public decimal? Discount { get; set; }
    public int Amount { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateOnly CreatedOn { get; set; }
}
