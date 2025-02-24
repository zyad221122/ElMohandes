namespace The_Engneering.Contracts.Product;

public class ProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } // حفظ مسار الصورة فقط
    public string CountryOfOrigin { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public IFormFile Cover { get; set; } = default!; // File upload  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
