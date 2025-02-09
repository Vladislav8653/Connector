using Application.Contracts;
using Application.Services;

namespace Connector;

public static class ServiceExtensions
{
    public static void ConfigureConnectorRestService(this IServiceCollection services)
    {
        services.AddScoped<ITestConnectorRest, TestConnectorRest>();
    }
}