namespace The_Engneering.Services;

public interface IBillService
{
    Task<IEnumerable<Bill>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Bill?> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Bill?> AddAsync(int id, Bill bill, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(string id, Bill bill, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
