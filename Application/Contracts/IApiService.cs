namespace Application.Contracts;

public interface IApiService
{
    Task<string?> GetTradesDataAsync(string pair, int maxCount, int? sort, DateTimeOffset? start, DateTimeOffset? end);
    Task<string?> GetCandleSeriesDataAsync(string pair, string timeFrame, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0, int? sort = null);
    Task<string?> GetTickerDataAsync(string pair);
}