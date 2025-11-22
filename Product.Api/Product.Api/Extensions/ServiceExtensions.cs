using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Product.Api.Persistence;
using Product.Api.Repositories;
using Product.Api.Services.CatalogProducts;
using AutoMapper;
using Product.Api.Persistence.Seeding;

namespace Product.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        // Configure CORS using settings from configuration (appsettings.json).
        // This allows different origins to be set per-environment (dev/staging/prod).
        var corsOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        var corsPolicyName = configuration.GetValue<string>("Cors:PolicyName") ?? "DefaultCorsPolicy";
        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyName, builder =>
            {
                if (corsOrigins.Length > 0)
                {
                    builder.WithOrigins(corsOrigins)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                }
                else
                {
                    // If no origins configured, fall back to allowing any origin.
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                }

                // Optional: allow credentials if configured
                var allowCredentials = configuration.GetValue<bool?>("Cors:AllowCredentials");
                if (allowCredentials == true)
                {
                    builder.AllowCredentials();
                }
            });
        });
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureProductDbContext(configuration);
        services.AddInfrastructureServices();

        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

        /*services.ConfigureSwagger();
        // services.AddJwtAuthentication();
        services.ConfigureAuthenticationHandler();
        services.ConfigureAuthorization();
        services.ConfigureHealthChecks();*/
        return services;
    }

    private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnectionString");

        services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductSeedingService, ProductSeedingService>();

        return services;
    }
}