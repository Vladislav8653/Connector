using Application.Contracts;
using Domain.Models;

namespace Application.Services;

public class TestConnectorRest : ITestConnectorRest
{
    private readonly IApiService _apiService;
    public TestConnectorRest(IApiService apiService)
    {
        _apiService = apiService;
    }
    public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
    {
        var content = await _apiService.GetTradesData(pair, maxCount);
        Console.WriteLine(content);
        throw new Exception("хуй");
    }

    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
    {
        throw new NotImplementedException();
    }
}