using System.Collections.Immutable;
using Application.Contracts;
using Application.Utilities;
using Domain.Models;

namespace Application.Services;

public class TestConnectorRest : ITestConnectorRest
{
    private readonly ImmutableHashSet<string> _timeFrames =
        ImmutableHashSet.Create("1m", "5m", "15m", "30m", "1h", "3h", "6h", "12h", "1D", "1W", "14D", "1M");
    
    private readonly IApiService _apiService;

    public TestConnectorRest(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount, int? sort = null,
        DateTimeOffset? start = null, DateTimeOffset? end = null)
    {
        var content = await _apiService.GetTradesDataAsync(pair, maxCount, sort, start, end);
        if (content is null)
            throw new ArgumentException("No trades found");
        var trades = new List<Trade>();
        var lines = StringUtility.GetDataLines(content);
        foreach (var line in lines)
        {
            var values = StringUtility.GetValuesFromLine(line);
            var amount = StringUtility.ConvertFloatToDecimal(values[2]);
            var trade = new Trade
            {
                Pair = pair,
                Price = StringUtility.ConvertFloatToDecimal(values[3]),
                Amount = amount,
                Side = Math.Sign(amount) == 1 ? "buy" : "sell",
                Time = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[1])),
                Id = values[0]
            };
            trades.Add(trade);
        }

        return trades;
    }

    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeFrame, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0, int? sort = null)
    {
        if (!_timeFrames.Contains(timeFrame))
            throw new ArgumentException("Invalid time frame");
        var content = await _apiService.GetCandleSeriesDataAsync(pair, timeFrame, from, to, count, sort);
        if (content is null)
            throw new ArgumentException("No candles found");
        var candles = new List<Candle>();
        var lines = StringUtility.GetDataLines(content);
        foreach (var line in lines)
        {
            var values = StringUtility.GetValuesFromLine(line);
            var closePrice = StringUtility.ConvertFloatToDecimal(values[2]);
            var totalVolume = StringUtility.ConvertFloatToDecimal(values[5]);
            var candle = new Candle
            {
                Pair = pair,
                OpenPrice = StringUtility.ConvertFloatToDecimal(values[1]),
                HighPrice = StringUtility.ConvertFloatToDecimal(values[3]),
                LowPrice = StringUtility.ConvertFloatToDecimal(values[4]),
                ClosePrice = closePrice,
                TotalPrice = closePrice * totalVolume,
                TotalVolume = totalVolume,
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[0])),
            };
            candles.Add(candle);
        }

        return candles;
    }

    public async Task<Ticker> GetTickerAsync(string pair)
    {
        var content = await _apiService.GetTickerDataAsync(pair);
        if (content is null)
            throw new ArgumentException("No ticker found");
        var values = StringUtility.GetValuesFromLine(content);
        var ticker = new Ticker
        {
            Pair = pair,
            Bid = StringUtility.ConvertFloatToDecimal(values[0]),
            BidSize = StringUtility.ConvertFloatToDecimal(values[1]),
            Ask = StringUtility.ConvertFloatToDecimal(values[2]),
            AskSize = StringUtility.ConvertFloatToDecimal(values[3]),
            DailyChange = StringUtility.ConvertFloatToDecimal(values[4]),
            DailyChangeRelative = StringUtility.ConvertFloatToDecimal(values[5]),
            LastPrice = StringUtility.ConvertFloatToDecimal(values[6]),
            Volume = StringUtility.ConvertFloatToDecimal(values[7]),
            High = StringUtility.ConvertFloatToDecimal(values[8]),
            Low = StringUtility.ConvertFloatToDecimal(values[9]),
        };
        return ticker;
    }
}