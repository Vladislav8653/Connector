using Application.Contracts;
using Application.Services;
using Infrastructure.ApiServices;
using Infrastructure.Configuration;

namespace Connector;

public static class ServiceExtensions
{
    public static void ConfigureConnectorRestService(this IServiceCollection services)
    {
        services.AddScoped<ITestConnectorRest, TestConnectorRest>();
    }
    
    public static void ConfigureExternalApiService(this IServiceCollection services)
    {
        services.AddScoped<IApiService, ApiService>();
    }

    public static void SetupConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeApiSettings>(configuration.GetSection("ExchangeApiSettings"));
    }
}