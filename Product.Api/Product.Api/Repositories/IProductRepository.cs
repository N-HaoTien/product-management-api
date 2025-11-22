using Product.Api.Entities;

namespace Product.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<CatalogProduct>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogProduct?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogProduct> AddAsync(CatalogProduct product, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogProduct product, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
