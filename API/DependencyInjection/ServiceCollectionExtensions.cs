using API.Database;
using API.Services;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;

namespace API.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        AddDatabase(services, configuration);
        AddLogging(services, environment);
        AddServices(services);
        // AddCache(services, configuration);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<ITaskService, TaskService>();
        services.AddTransient<IAuthService, AuthService>();
        // services.AddTransient<ICatalogService, CatalogService>();
        // services.AddTransient<IBasketService, BasketService>();
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                opts => { opts.MigrationsAssembly("API"); });
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    private static void AddLogging(IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddLogging(options =>
        {
            options.ClearProviders();
            options.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new EnvironmentLoggingLevelSwitch(environment))
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Conditional(l => environment.IsDevelopment(), c => c.Console())
                .CreateLogger());
        });
    }

    internal class EnvironmentLoggingLevelSwitch : LoggingLevelSwitch
    {
        public EnvironmentLoggingLevelSwitch(IWebHostEnvironment environment)
        {
            MinimumLevel = environment.IsDevelopment()
                ? LogEventLevel.Debug
                : LogEventLevel.Information;
        }
    }
}