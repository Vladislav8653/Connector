namespace Infrastructure.Configuration;

public class ExchangeApiSettings
{
    public string BaseUrl { get; set; }
    public string Version { get; set; }
    public Endpoints Endpoints { get; set; }
}

