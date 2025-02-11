using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class ConnectorController(ITestConnectorRest connectorRest, ITestConnectorWs connectorWs) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await connectorWs.ConnectAsync();
        await connectorWs.SubscribeTradesAsync("BTCUSD");
        return Ok();
    }
}