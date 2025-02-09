namespace Application.Contracts;

public interface IApiService
{
    Task<string?> GetTradesData(string pair, int maxCount, int? sort, int? start, int? end);
}