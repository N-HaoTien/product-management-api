using Microsoft.EntityFrameworkCore;
using Product.Api.Entities;
using Product.Api.Persistence;

namespace Product.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _db;

    public ProductRepository(ProductDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CatalogProduct>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.CatalogProducts.AsNoTracking().OrderByDescending(p => p.Id).ToListAsync(cancellationToken);
    }

    public async Task<CatalogProduct?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        // Use FirstOrDefaultAsync with AsNoTracking to ensure proper async behavior and cancellation support
        return await _db.CatalogProducts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<CatalogProduct> AddAsync(CatalogProduct product, CancellationToken cancellationToken = default)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        var entry = await _db.CatalogProducts.AddAsync(product, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task UpdateAsync(CatalogProduct product, CancellationToken cancellationToken = default)
    {
        _db.CatalogProducts.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return;
        _db.CatalogProducts.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
