using AutoMapper;
using Product.Api.Entities;
using Product.Api.Models;
using Product.Api.Repositories;

namespace Product.Api.Services.CatalogProducts;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CatalogProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _productRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CatalogProductDto>>(items);
    }

    public async Task<CatalogProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _productRepository.GetByIdAsync(id, cancellationToken);
        return item is null ? null : _mapper.Map<CatalogProductDto>(item);
    }

    public async Task<CatalogProductDto> CreateAsync(CreateCatalogProductDto dto,
        CancellationToken cancellationToken = default)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        var entity = _mapper.Map<CatalogProduct>(dto);
        var created = await _productRepository.AddAsync(entity, cancellationToken);
        return _mapper.Map<CatalogProductDto>(created);
    }

    public async Task UpdateAsync(UpdateCatalogProductDto dto, CancellationToken cancellationToken = default)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        var existing = await _productRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (existing is null) throw new KeyNotFoundException($"Product with id {dto.Id} not found.");

        var updated = _mapper.Map(dto, existing);
        await _productRepository.UpdateAsync(updated, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _productRepository.DeleteAsync(id, cancellationToken);
    }
}