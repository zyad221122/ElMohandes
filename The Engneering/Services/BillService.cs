using The_Engneering.Entities;
namespace The_Engneering.Services;

public class BillService(ApplicationDbContext _context) : IBillService
{
    private readonly ApplicationDbContext context = _context;
    public async Task<IEnumerable<Bill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Bills.AsNoTracking().OrderByDescending(b => b.CreatedOn).ToListAsync();
    }
    public async Task<Bill?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.Bills.FindAsync(id);
    }
    public async Task<Bill?> AddAsync(int id, Bill bill, CancellationToken cancellationToken = default)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
            return null;
        bill.ProductName = product.Name;
        bill.Price = product.Price;
        bill.Discount = product.Discount;
        var priceWithOutDiscount = product.Price * bill.Amount;
        var discountPercentage = priceWithOutDiscount * (product.Discount / 100);
        bill.TotalPrice = priceWithOutDiscount- discountPercentage;
        product.Quantity -= bill.Amount;
        await context.Bills.AddAsync(bill, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return bill;
    }
    public async Task<bool> UpdateAsync(string id, Bill bill, CancellationToken cancellationToken = default)
    {
        var currentBill = await GetAsync(id, cancellationToken);

        if (currentBill is null)
            return false;
        currentBill.CustomerName = bill.CustomerName;
        currentBill.CustomerPhone = bill.CustomerPhone;
        currentBill.PayType = bill.PayType;
        currentBill.ProductName = bill.ProductName;
        currentBill.Amount = bill.Amount;
        var priceWithOutDiscount = currentBill.Price * currentBill.Amount;
        var discountPercentage = priceWithOutDiscount * (currentBill.Discount / 100);
        currentBill.TotalPrice = priceWithOutDiscount - discountPercentage;
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);
        if (poll is null)
            return false;
        context.Bills.Remove(poll);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}