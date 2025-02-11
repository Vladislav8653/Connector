namespace Application.Contracts;

public interface IApiServiceWs
{
    Task SubscribeTradesAsync(string pair);
    Task UnsubscribeTradesAsync(string pair);
    Task SubscribeCandlesAsync(string pair, string timeFrame);
    Task UnsubscribeCandlesAsync(string pair);
    Task ConnectAsync();
    Task DisconnectAsync();
    public event Action<string> OnMessageReceived;
}