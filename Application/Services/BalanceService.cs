using Application.Contracts;
using Application.DTO;
using Application.Utilities;

namespace Application.Services;

public class BalanceService(IApiServiceRest apiServiceRest)
{
    private readonly BalanceDto _btc = new("BTC", 1);
    private readonly BalanceDto _xrp = new("XRP", 15000);
    private readonly BalanceDto _xmr = new("XMR", 50);
    private readonly BalanceDto _dash = new("DSH", 30);

    public async Task< List<BalanceDto>> GetBalance()
    {
        var balance = new List<BalanceDto>
        {
            await GetCurrencySum(_btc.Currency, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_xrp.Currency, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_xmr.Currency, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum(_dash.Currency, _btc, _xrp, _xmr, _dash),
            await GetCurrencySum("USD", _btc, _xrp, _xmr, _dash)
        };
        return balance;
    }

    private async Task<BalanceDto> GetCurrencySum(string name, params BalanceDto[] currencies)
    {
        decimal sum = 0;
        foreach (var currency in currencies)
        {
            var result = await apiServiceRest.GetExchangeRate(currency.Currency, name);
            if (result is null)
                throw new ArgumentException("Exchange error");
            var value = StringUtility.GetValuesFromLine(result)[0];
            if (value != "null")
                sum += StringUtility.ConvertFloatToDecimal(value);
        }

        return new BalanceDto(name, sum);
    }
    
}