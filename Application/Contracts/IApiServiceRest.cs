namespace Application.Contracts;

public interface IApiServiceRest
{
    Task<string?> GetTradesDataAsync(string pair, int maxCount, int? sort, DateTimeOffset? start, DateTimeOffset? end);
    Task<string?> GetCandleSeriesDataAsync(string pair, string timeFrame, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0, int? sort = null);
    Task<string?> GetTickerDataAsync(string pair);
    Task<string?> GetExchangeRate(string ccy1, string ccy2);
}