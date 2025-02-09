using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class Connector : ControllerBase
{
    private readonly ITestConnectorRest _connectorRest;
    public Connector(ITestConnectorRest connectorRest)
    {
        _connectorRest = connectorRest;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var trades = await _connectorRest.GetNewTradesAsync("BTCUSD", 125);
        return Ok(trades);
    }
}