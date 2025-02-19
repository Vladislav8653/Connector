namespace Application.DTO.RestConnector;

public class CandlesDto
{
    public string Pair { get; set; }
    public string TimeFrame { get; set; }
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public long? Count {get; set;}
    public int? Sort { get; set; }
}