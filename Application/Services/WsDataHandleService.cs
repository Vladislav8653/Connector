using System.Text.Json;
using Application.Contracts;
using Application.DTO;
using Application.Utilities;
using Domain.Models;

namespace Application.Services;

public class WsDataHandleService : IWsDataHandleService
{
    private Dictionary<string, WsData> _channels = []; // "chanId, (channel, pair)"
    
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public event Action<Candle>? CandleSeriesProcessing;
    
    public void OnMessageReceived(string message)
    {
        Console.WriteLine(message);

        using (JsonDocument doc = JsonDocument.Parse(message))
        {
            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                HandleObjectJson(doc.RootElement);
            }
            else if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                HandleArrayJson(doc.RootElement);
            }
        }
    }
    
    private void HandleArrayJson(JsonElement data)
    {
        Console.WriteLine("array");
        var dataArr = data.EnumerateArray().ToList();

        if (dataArr.Count == 2 && dataArr[1].ToString() == "hb")
        {
            return;
        }
        if (_channels.TryGetValue(dataArr[0].ToString(), out var channel))
        {
            switch (channel.Channel)
            {
                case "trades":
                {
                    var values = StringUtility.GetValuesFromLine(dataArr[0].ToString());
                    InvokeTrade(values, channel.Pair);
                    break;
                }
                case "candles":
                {
                    var values = StringUtility.GetValuesFromLine(dataArr[0].ToString());
                    InvokeCandles(values, channel.Pair);
                    break;
                }
            }
        }
    }

    private void HandleObjectJson(JsonElement data)
    {
        Console.WriteLine("object");
        if (data.TryGetProperty("event", out var eventProperty) && eventProperty.GetString() == "subscribed")
        {
            if (data.TryGetProperty("chanId", out var chanIdProperty) &&
                data.TryGetProperty("channel", out var channelProperty) &&
                data.TryGetProperty("pair", out var pairProperty))
            {
                var chanId = chanIdProperty.ToString();
                var channel = channelProperty.ToString();
                var pair = pairProperty.ToString();
                _channels.Add(chanId, new WsData
                {
                    Channel = channel,
                    Pair = pair
                });
                Console.WriteLine($"Подписка успешна! chanId: {chanId}");
            }
        }
    }

    private void InvokeTrade(string[] values, string pairName)
    {
        var amount = StringUtility.ConvertFloatToDecimal(values[2]);
        var trade = new Trade
        {
            Pair = pairName,
            Price = StringUtility.ConvertFloatToDecimal(values[3]),
            Amount = amount,
            Side = Math.Sign(amount) == 1 ? "buy" : "sell",
            Time = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[1])),
            Id = values[0]
        };
        if (Math.Sign(amount) == 1)
            NewBuyTrade?.Invoke(trade);
        else 
            NewSellTrade?.Invoke(trade);
    }

    private void InvokeCandles(string[] values, string pairName)
    {
        var closePrice = StringUtility.ConvertFloatToDecimal(values[2]);
        var totalVolume = StringUtility.ConvertFloatToDecimal(values[5]);
        var candle = new Candle
        {
            Pair = pairName,
            OpenPrice = StringUtility.ConvertFloatToDecimal(values[1]),
            HighPrice = StringUtility.ConvertFloatToDecimal(values[3]),
            LowPrice = StringUtility.ConvertFloatToDecimal(values[4]),
            ClosePrice = closePrice,
            TotalPrice = closePrice * totalVolume,
            TotalVolume = totalVolume,
            OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(values[0])),
        };
        CandleSeriesProcessing?.Invoke(candle);
    }
}