using AiLab.Application.Repositories;
using AiLab.Application.Services;
using AiLab.Health;
using Microsoft.EntityFrameworkCore;

namespace AiLab.Extensions;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Ecommerce");
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<global::AiLab.Data.Context.AiLab>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions =>
                    mysqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));
        }

        // Repositories and services
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddSingleton<Application.Services.Validation.IBrandValidation, Application.Services.Validation.BrandValidation>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();
        services.AddSingleton<Application.Services.Validation.IItemValidation, Application.Services.Validation.ItemValidation>();
        services.AddSingleton<Application.Services.Validation.IStockValidation, Application.Services.Validation.StockValidation>();

        // Health check for database
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database");

        return services;
    }
}
