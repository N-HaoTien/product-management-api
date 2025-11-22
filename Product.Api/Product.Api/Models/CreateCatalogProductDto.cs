using System.ComponentModel.DataAnnotations;

namespace Product.Api.Models;

public record CreateCatalogProductDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; init; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; init; } = string.Empty;

    [Range(0, double.MaxValue)]
    public double Price { get; init; }
}
