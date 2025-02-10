using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class ConnectorController(ITestConnectorRest connectorRest, ITestConnectorWS connectorWs) : ControllerBase
{
    private readonly ITestConnectorRest _connectorRest = connectorRest;

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        /*connectorWs.SubscribeTrades("BTCUSD");
        Thread.Sleep(60000);
        connectorWs.UnsubscribeTrades("BTCUSD");*/
        return Ok();
    }
}