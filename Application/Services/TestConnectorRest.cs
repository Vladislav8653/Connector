using System.Globalization;
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

    public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount, int? sort = null,
        int? start = null, int? end = null)
    {
        var content = await _apiService.GetTradesData(pair, maxCount, sort, start, end);
        if (content is null)
            throw new Exception("No trades found"); // добавить миддлваре
        var trades = new List<Trade>();
        var lines = content.Split(["],", "], "], StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines) // можно преобразовать в LINQ, но с циклом понятнее выглядит алгоритм
        {
            var trimmedLine = line.Trim().Trim('[', ']');
            var values = trimmedLine.Split(',');
            var amount = (decimal)Convert.ToDouble(values[2].Replace(".", ","));
            var trade = new Trade
            {
                Pair = pair,
                Price = Convert.ToDecimal(values[3]),
                Amount = amount, 
                Side = Math.Sign(amount) == 1 ? "buy" : "sell",
                Time = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[1])),
                Id = values[0]
            };
            trades.Add(trade);
        }
        return trades;
    }

    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0)
    {
        throw new NotImplementedException();
    }
}