using Product.Api.Models;

namespace Product.Api.Services.CatalogProducts;

public interface IProductService
{
    Task<IEnumerable<CatalogProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogProductDto> CreateAsync(CreateCatalogProductDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateCatalogProductDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
