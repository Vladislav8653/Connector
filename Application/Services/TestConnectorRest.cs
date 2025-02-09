using Application.Contracts;
using Domain.Models;
using RestSharp;

namespace Application.Services;

public class TestConnectorRest : ITestConnectorRest
{
    public TestConnectorRest()
    {
        
    }
    public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
    {
        var baseUrl = new Uri($"https://api-pub.bitfinex.com/v2/trades/t{pair}/hist?limit={maxCount}&sort=-1");
        var options = new RestClientOptions(baseUrl);
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);
        Console.WriteLine(response.Content);
        throw new Exception("хуй");
    }

    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
    {
        throw new NotImplementedException();
    }
}