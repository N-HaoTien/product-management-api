namespace Product.Api.Extensions;

using Microsoft.EntityFrameworkCore;
using Product.Api.Persistence;

public static class ApplicationExtensions
{
    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        // Global exception handler middleware
        app.UseMiddleware<Product.Api.Middleware.ExceptionHandlingMiddleware>();

        // Apply EF migrations and seed data at startup. Run async calls synchronously here because
        // this method is not async and we need migrations to be applied before the app starts handling requests.
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<ProductDbContext>();
                // Run migrations
                db.Database.MigrateAsync().GetAwaiter().GetResult();

                // Run optional seeders
                var seeder = services.GetService<Product.Api.Persistence.Seeding.IProductSeedingService>();
                seeder?.SeedAsync().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                // If migration/seeding fails, allow the exception to bubble (or log as needed).
                throw;
            }
        }

    app.UseRouting();

    // Enable CORS using the policy configured in AddInfrastructure.
    // This must be placed between UseRouting() and UseEndpoints().
    var corsPolicyName = app.ApplicationServices.GetService<IConfiguration>()?.GetValue<string>("Cors:PolicyName") ?? "DefaultCorsPolicy";
    app.UseCors(corsPolicyName);

    app.UseEndpoints(endpoints =>
        {
            /*endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });*/
            endpoints.MapDefaultControllerRoute();
        });
    }
}