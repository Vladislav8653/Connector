namespace Application.Contracts;

public interface IApiService
{
    Task<string?> GetTradesData(string pair, int maxCount, int? sort, int? start, int? end);
    Task<string?> GetCandleSeries(string pair, int periodInSec, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0);
}