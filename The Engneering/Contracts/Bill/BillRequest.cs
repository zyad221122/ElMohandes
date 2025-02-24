namespace The_Engneering.Contracts.Bill;

public class BillRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string PayType { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
}
