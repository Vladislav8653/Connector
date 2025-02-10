using Domain.Models;

namespace Application.Contracts;

public interface ITestConnectorRest
{
    #region Rest

    // Добавлены параметры sort, start, sort, так как без них теряется определенный функционал стороннего API 
    Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount, int? sort = null, DateTimeOffset? start = null, DateTimeOffset? end = null);
   
    // Добавлены параметры sort и timeframe. Удалён period, так как такого параметра для валютной пары нет
    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, string timeFrame, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0, int? sort = null);
   
    // Добавлен этот метод, так как по условию нужна информация о тикере, а в исходном интерфейсе ее нет 
    Task<Ticker> GetTickerAsync(string pair);
    #endregion
}