using Application.Contracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Infrastructure.ApiServices;

public class ApiService : IApiService
{
    private readonly ExchangeApiSettings _config;
    
    public ApiService(IOptions<ExchangeApiSettings> settings)
    {
        _config = settings.Value;
    }
    
    public async Task<string?> GetTradesData(string pair, int maxCount)
    {
        var url = new Uri($"{_config.BaseUrl}{_config.Version}{_config.Endpoints.Trades}/t{pair}/hist?limit={maxCount}&sort=-1");
        var baseUrl = new Uri($"https://api-pub.bitfinex.com/v2/trades/t{pair}/hist?limit={maxCount}&sort=-1");
        var options = new RestClientOptions(url);
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);
        return response.Content;
    }
}