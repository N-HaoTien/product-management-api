using System.ComponentModel.DataAnnotations;

namespace Product.Api.Models;

public record CatalogProductDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public double Price { get; init; }
}
