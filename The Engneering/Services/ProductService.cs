
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Metrics;
using The_Engneering.Contracts.Product;

namespace The_Engneering.Services;

public class ProductService(IWebHostEnvironment _webHostEnvironment, ApplicationDbContext _context) : IProductService
{
    private readonly string _imagesPath = $"{_webHostEnvironment.WebRootPath}/images";//path of folder (images)
    private readonly ApplicationDbContext context = _context;
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }
    public async Task<Product?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Products.FindAsync(id);
    }
    public async Task<Product> AddAsync(Product product,  CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }
    public async Task<bool> UpdateAsync(int id, Product product, CancellationToken cancellationToken = default)
    {
        var currentProduct = await GetAsync(id, cancellationToken);

        if (currentProduct is null)
            return false;

        currentProduct.Name = product.Name;
        currentProduct.Quantity = product.Quantity;
        currentProduct.CountryOfOrigin = product.CountryOfOrigin;
        currentProduct.Discount = product.Discount;
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var isDeleted = false;
        var currentProduct = await GetAsync(id, cancellationToken);
        if (currentProduct is null)
            return isDeleted;
        context.Products.Remove(currentProduct);
        var effectedRows =  await context.SaveChangesAsync(cancellationToken);
        if (effectedRows > 0)
        {
            isDeleted = true;
            var cover = Path.Combine(_imagesPath, currentProduct.ImageUrl);
            File.Delete(cover);
        }
        return isDeleted;
    }
    public async Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var path = Path.Combine(_imagesPath, fileName);
        using var stream = File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
        return fileName;
    }
}