using Application.Contracts;
using Domain.Models;

namespace Application.Services;

public class TestConnectorWs(IApiServiceWs apiService) : ITestConnectorWs
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
        apiService.OnMessageReceived += OnMessageReceived;
    }  
    
    public async Task DisconnectAsync()
    {
        await apiService.DisconnectAsync();
    }
    
    private void OnMessageReceived(string message)
    {
        Console.WriteLine(message);
    }
}