using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Application.Contracts;
using Domain.Models;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.ApiServices;

public class ApiServiceWs : IApiServiceWs
{
    private readonly ClientWebSocket _webSocket;
    private readonly string _url;
    public ApiServiceWs(IOptions<ExchangeApiSettings> settings)
    {
        _webSocket = new ClientWebSocket();
        _url = settings.Value.WebSocketUrl;
    }
    
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public event Action<Candle>? CandleSeriesProcessing;


    public async Task ConnectAsync()
    {
        await _webSocket.ConnectAsync(new Uri(_url), CancellationToken.None);
        _ = Task.Run(ReceiveMessagesAsync);
    }
    
    public async Task DisconnectAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            Console.WriteLine("Disconnected from the server.");
        }
    }
    
    public async Task SubscribeTradesAsync(string pair)
    {
        var request = JsonSerializer.Serialize(new
        {
            @event = "subscribe",
            channel = "ticker",
            symbol = $"t{pair}",
        });
        await SendMessageAsync(request);
    }

    public async Task UnsubscribeTradesAsync(string pair)
    {
        var request = JsonSerializer.Serialize(new
        {
            @event = "unsubscribe",
            channel = "ticker",
            symbol = $"t{pair}"
        });
        await SendMessageAsync(request);
    }

    public async Task SubscribeCandlesAsync(string pair, string timeFrame)
    {
        var key = $"trade:{timeFrame}:t{pair}";
        var request = JsonSerializer.Serialize(new
        {
            @event = "subscribe",
            channel = "candles",
            key = key
        });
        await SendMessageAsync(request);
    }

    public async Task UnsubscribeCandlesAsync(string pair)
    {
        var request = JsonSerializer.Serialize(new
        {
            @event = "unsubscribe",
            channel = "candles",
        });
        await SendMessageAsync(request);
    }

    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[1024];

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
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
                CancellationToken.None);
            Console.WriteLine($"Sent: {message}");
        }
    }
}