using Domain.Models;

namespace Application.Contracts;

public interface IApiServiceWs
{
    event Action<Trade> NewBuyTrade;
    event Action<Trade> NewSellTrade;
    Task SubscribeTradesAsync(string pair);
    Task UnsubscribeTradesAsync(string pair);
    event Action<Candle> CandleSeriesProcessing;
    Task SubscribeCandlesAsync(string pair, string timeFrame);
    Task UnsubscribeCandlesAsync(string pair);
    
    Task ConnectAsync();

    Task DisconnectAsync();
}