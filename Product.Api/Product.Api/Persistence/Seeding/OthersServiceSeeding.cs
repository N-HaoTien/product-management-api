using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Api.Entities;

namespace Product.Api.Persistence.Seeding;

public interface IOthersServiceSeeding
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}

public class OthersServiceSeeding : IOthersServiceSeeding
{
    private readonly ProductDbContext _db;

    public OthersServiceSeeding(ProductDbContext db)
    {
        _db = db;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        // Simple seed: only seed when empty
        if (await _db.CatalogProducts.AnyAsync(cancellationToken))
            return;

        var now = DateTime.UtcNow;

        var items = new List<CatalogProduct>
        {
            new CatalogProduct { Name = "Classic White Shirt", Description = "Comfortable cotton shirt", Price = 39.99 },
            new CatalogProduct { Name = "Blue Denim Jeans", Description = "Slim-fit denim", Price = 69.99 },
            new CatalogProduct { Name = "Leather Belt", Description = "Genuine leather", Price = 29.5 }
        };

        await _db.CatalogProducts.AddRangeAsync(items, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
