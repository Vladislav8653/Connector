using Domain.Models;

namespace Application.Contracts;

public interface IWsDataHandleService
{
    public void OnMessageReceived(string message);
    public event Action<Trade>? NewBuyTrade;
    public event Action<Trade>? NewSellTrade;
    public event Action<Candle>? CandleSeriesProcessing;
}