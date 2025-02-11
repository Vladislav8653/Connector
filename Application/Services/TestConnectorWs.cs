using Application.Contracts;
using Domain.Models;

namespace Application.Services;

public class TestConnectorWs(IApiServiceWs apiService, IWsDataHandleService handleService) : ITestConnectorWs
{ 
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public event Action<Candle>? CandleSeriesProcessing;

    public async Task SubscribeTradesAsync(string pair)
    {
        await apiService.SubscribeTradesAsync(pair);
    }

    public async Task UnsubscribeTradesAsync(string pair)
    {
        await apiService.UnsubscribeTradesAsync(pair);
    }

    public async Task SubscribeCandlesAsync(string pair, string timeFrame)
    {
        await apiService.SubscribeCandlesAsync(pair, timeFrame);
    }

    public async Task UnsubscribeCandlesAsync(string pair)
    {
        await apiService.UnsubscribeCandlesAsync(pair);
    }

    public async Task ConnectAsync()
    {
        await apiService.ConnectAsync();
        apiService.OnMessageReceived += handleService.OnMessageReceived;
        handleService.NewBuyTrade += NewBuyTrade;
        handleService.NewSellTrade += NewSellTrade;
        handleService.CandleSeriesProcessing += CandleSeriesProcessing;
    }

    public async Task DisconnectAsync()
    {
        await apiService.DisconnectAsync();
    }
    
}