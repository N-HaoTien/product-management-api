using Product.Api.Core.Domain;

namespace Product.Api.Entities;

public class CatalogProduct : EntityBase<int>
{
    // Provide safe defaults to satisfy nullable analysis and avoid nulls in persistence
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
}