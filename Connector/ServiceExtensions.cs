using Application.Contracts;
using Application.Services;
using Infrastructure.ApiServices;
using Infrastructure.Configuration;

namespace Connector;

public static class ServiceExtensions
{
    public static void ConfigureConnectorService(this IServiceCollection services)
    {
        services.AddScoped<ITestConnectorRest, TestConnectorRest>();
        services.AddScoped<ITestConnectorWS, TestConnectorWS>();
    }
    
    public static void ConfigureExternalApiService(this IServiceCollection services)
    {
        services.AddScoped<IApiServiceRest, ApiServiceRest>();
        services.AddSingleton<IApiServiceWs, ApiServiceWs>();
    }

    public static void SetupConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeApiSettings>(configuration.GetSection("ExchangeApiSettings"));
    }
}