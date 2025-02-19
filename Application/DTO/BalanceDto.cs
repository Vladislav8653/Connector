namespace Application.DTO;

public class BalanceDto
{
    public BalanceDto(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}