using Domain.Models;

namespace Application.Contracts;

public interface ITestConnectorWs
{
    #region Socket
    // синхронные методы заменены асинхронными
    event Action<Trade> NewBuyTrade;
    event Action<Trade> NewSellTrade;
    
    // в документации ничего не сказано про параметр maxCount, поэтому он был удалён
    Task SubscribeTradesAsync(string pair);
    Task UnsubscribeTradesAsync(string pair);
    event Action<Candle> CandleSeriesProcessing;
    
    // Добавлен параметр timeframe. 
    // Также не ясно, зачем нужны остальные параметры, так как в вебсокетах нужна только пара валют и timeframe
    Task SubscribeCandlesAsync(string pair, string timeFrame);
    Task UnsubscribeCandlesAsync(string pair);

    // добавлены методы для установки и разрыва соединения 
    Task ConnectAsync();

    Task DisconnectAsync();
    
    #endregion

}