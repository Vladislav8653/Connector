using Domain.Models;

namespace Application.Contracts;

public interface IApiServiceWs
{
    event Action<Trade> NewBuyTrade;
    event Action<Trade> NewSellTrade;
    void SubscribeTrades(string pair, long? maxCount);
    void UnsubscribeTrades(string pair);
    event Action<Candle> CandleSeriesProcessing;
    void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0);
    void UnsubscribeCandles(string pair);
}