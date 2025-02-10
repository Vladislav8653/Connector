using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class ConnectorController(ITestConnectorRest connectorRest, ITestConnectorWS connectorWs) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
       
        return Ok();
    }
}