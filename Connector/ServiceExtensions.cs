using Application.Contracts;
using Application.Services;
using Application.Utilities;
using Infrastructure.ApiServices;
using Infrastructure.Configuration;

namespace Connector;

public static class ServiceExtensions
{
    public static void ConfigureConnectorServices(this IServiceCollection services)
    {
        services.AddScoped<ITestConnectorRest, TestConnectorRest>();
        services.AddScoped<ITestConnectorWs, TestConnectorWs>();
    }

    public static void ConfigureBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<BalanceService>();
    }
    
    public static void ConfigureExternalApiServices(this IServiceCollection services)
    {
        services.AddScoped<IApiServiceRest, ApiServiceRest>();
        services.AddSingleton<IApiServiceWs, ApiServiceWs>();
        services.AddSingleton<IWsDataHandleService, WsDataHandleService>();
    }

    public static void SetupConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeApiSettings>(configuration.GetSection("ExchangeApiSettings"));
    }
}