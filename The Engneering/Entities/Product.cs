namespace The_Engneering.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty; // حفظ مسار الصورة فقط
    public string CountryOfOrigin { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
