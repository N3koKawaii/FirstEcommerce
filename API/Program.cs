using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<EcommerceContext>(options
    => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging());
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// -- Migrate the database on startup
using (var scope = app.Services.CreateScope())
{
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<EcommerceContext>();
        await context.Database.MigrateAsync();
        await EcommerceContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();