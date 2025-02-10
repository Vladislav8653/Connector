using Application.Contracts;
using Domain.Models;

namespace Application.Services;

public class TestConnectorWS(IApiServiceWs apiService) : ITestConnectorWS
{
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    
    public void SubscribeTrades(string pair, int maxCount = 100)
    {
        apiService.SubscribeTrades(pair, maxCount);
    }

    public void UnsubscribeTrades(string pair)
    {
        apiService.UnsubscribeTrades($"unsubscribe:{pair}");
    }

    public event Action<Candle>? CandleSeriesProcessing;

    public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeCandles(string pair)
    {
        throw new NotImplementedException();
    }
}