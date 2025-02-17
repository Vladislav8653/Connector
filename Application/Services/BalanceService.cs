using Application.Contracts;
using Application.Utilities;

namespace Application.Services;

public class BalanceService(IApiServiceRest apiServiceRest)
{
    private readonly (string, decimal) _btc = ("BTC", 1);
    private readonly (string, decimal) _xrp = ("XRP", 15000);
    private readonly (string, decimal) _xmr = ("XMR", 50);
    private readonly (string, decimal) _dash = ("DSH", 30);

    public async Task< List<(string, decimal)>> GetBalance()
    {
        var balance = new List<(string, decimal)>
        {
            await GetCurrencySum(_btc.Item1, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_xrp.Item1, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_xmr.Item1, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_dash.Item1, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum("USD", _btc, _xrp, _xmr, _dash)
        };
        return balance;
    }

    private async Task<(string, decimal)> GetCurrencySum(string name, params (string, decimal)[] currencies)
    {
        decimal sum = 0;
        foreach (var currency in currencies)
        {
            var result = await apiServiceRest.GetExchangeRate(currency.Item1, name);
            if (result is null)
                throw new ArgumentException("Exchange error");
            var value = StringUtility.GetValuesFromLine(result)[0];
            if (value != "null")
                sum += StringUtility.ConvertFloatToDecimal(value);
        }

        return (name, sum);
    }
    
}