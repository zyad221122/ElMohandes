using The_Engneering.Contracts.Product;

namespace The_Engneering.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product,  CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<string> UploadImageAsync(IFormFile file, CancellationToken cancellationToken);
    //Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default);
}
