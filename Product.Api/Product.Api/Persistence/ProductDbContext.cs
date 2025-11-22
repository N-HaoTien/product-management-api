using Microsoft.EntityFrameworkCore;
using Product.Api.Entities;

namespace Product.Api.Persistence;

public class ProductDbContext : DbContext
{
	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
	{
	}

	public DbSet<CatalogProduct> CatalogProducts { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Apply entity configurations from the Persistence assembly
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
	}
}