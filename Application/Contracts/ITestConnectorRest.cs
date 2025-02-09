using Domain.Models;

namespace Application.Contracts;

public interface ITestConnectorRest
{
    #region Rest

    // Добавлены параметры sort, start, sort, так как без них теряется определенный функционал стороннего API 
    Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount, int? sort = null, int? start = null, int? end = null);
    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);

    #endregion
}