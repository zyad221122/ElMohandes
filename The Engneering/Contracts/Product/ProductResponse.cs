namespace The_Engneering.Contracts.Product;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty; // حفظ مسار الصورة فقط
    public string CountryOfOrigin { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}
