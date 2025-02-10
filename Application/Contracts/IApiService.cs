namespace Application.Contracts;

public interface IApiService
{
    Task<string?> GetTradesData(string pair, int maxCount, int? sort, long? start, long? end);
    Task<string?> GetCandleSeries(string pair, string timeFrame, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0, int? sort = null);
}