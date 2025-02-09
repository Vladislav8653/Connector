using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class ConnectorController : ControllerBase
{
    private readonly ITestConnectorRest _connectorRest;
    public ConnectorController(ITestConnectorRest connectorRest)
    {
        _connectorRest = connectorRest;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var candles = await _connectorRest.GetCandleSeriesAsync("BTCUSD", 100, new DateTimeOffset());
        return Ok(candles);
    }
}