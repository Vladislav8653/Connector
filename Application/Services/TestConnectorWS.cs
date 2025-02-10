using Application.Contracts;
using Domain.Models;

namespace Application.Services;

public class TestConnectorWS(IApiServiceWs apiService) : ITestConnectorWS
{
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public async Task SubscribeTradesAsync(string pair)
    {
        throw new NotImplementedException();
    }

    public async Task UnsubscribeTradesAsync(string pair)
    {
        throw new NotImplementedException();
    }

    public event Action<Candle>? CandleSeriesProcessing;
    public async Task SubscribeCandlesAsync(string pair, string timeFrame)
    {
        throw new NotImplementedException();
    }

    public async Task UnsubscribeCandlesAsync(string pair)
    {
        throw new NotImplementedException();
    }

    public async Task ConnectAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DisconnectAsync()
    {
        throw new NotImplementedException();
    }
}