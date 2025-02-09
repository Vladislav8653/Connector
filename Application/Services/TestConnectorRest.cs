using Application.Contracts;
using Application.Utilities;
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
            throw new ArgumentException("No trades found"); 
        var trades = new List<Trade>();
        var lines = StringUtility.GetDataLines(content);
        foreach (var line in lines) // можно преобразовать в LINQ, но с циклом понятнее выглядит алгоритм
        {
            var values = StringUtility.GetValuesFromLine(line);
            var amount = StringUtility.ConvertFloatToDecimal(values[2]);
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
        var content = await _apiService.GetCandleSeries(pair, periodInSec, from, to, count);
        if (content is null)
            throw new ArgumentException("No candles found");
        var candles = new List<Candle>();
        var lines = StringUtility.GetDataLines(content);
        foreach (var line in lines)
        {
            var values = StringUtility.GetValuesFromLine(line);
            var closePrice = Convert.ToDecimal(values[2]);
            var totalVolume = StringUtility.ConvertFloatToDecimal(values[5]);
            var candle = new Candle
            {
                Pair = pair,
                OpenPrice = Convert.ToDecimal(values[1]),
                HighPrice = Convert.ToDecimal(values[3]),
                LowPrice = Convert.ToDecimal(values[4]),
                ClosePrice = closePrice,
                TotalPrice = closePrice * totalVolume,
                TotalVolume = totalVolume,
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[0])),
            };
            candles.Add(candle);
        }
        return candles;
    }
}