namespace Application.DTO.RestConnector;

public class TradesDto
{
    public string Pair {get; set;}
    public int MaxCount { get; set; }
    public int? Sort { get; set; }
    public DateTimeOffset? Start { get; set; }
    public DateTimeOffset? End { get; set; }
}