using System.Net.WebSockets;
using System.Text;
using Application.Contracts;
using Domain.Models;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
//using WebSocketSharp;

namespace Infrastructure.ApiServices;

public class ApiServiceWs : IApiServiceWs
{
    private readonly ClientWebSocket _webSocket;

    public ApiServiceWs(IOptions<ExchangeApiSettings> settings)
    {
        _webSocket = new ClientWebSocket();
        _webSocket.ConnectAsync(new Uri(settings.Value.WebSocketUrl), CancellationToken.None).Wait();
        _ = Task.Run(ReceiveMessagesAsync);
    }

    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public event Action<Candle>? CandleSeriesProcessing;
    
    public void SubscribeTrades(string pair, long? maxCount)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeTrades(string pair)
    {
        throw new NotImplementedException();
    }

    public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null,
        long? count = 0)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeCandles(string pair)
    {
        throw new NotImplementedException();
    }
    
    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[1024 * 4];

        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                Console.WriteLine("Connection closed.");
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received: {message}");
                // Здесь вы можете обработать полученное сообщение
            }
        }
    }
    
    private async Task SendMessageAsync(string message)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine($"Sent: {message}");
        }
    }
}