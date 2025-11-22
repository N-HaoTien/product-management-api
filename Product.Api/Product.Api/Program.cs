using Product.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register infrastructure (controllers, swagger, dbcontext, automapper, services)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline and middleware from extensions
if (app.Environment.IsDevelopment())
{
    // Developer exception page in development
    app.UseDeveloperExceptionPage();
}

// Keep HTTPS redirection for production/dev when certificates are set
app.UseHttpsRedirection();

// Use the centralized app configuration from ApplicationExtensions
app.UseInfrastructure();

app.Run();