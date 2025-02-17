using Application.Contracts;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"api")]
public class ConnectorController(ITestConnectorRest connectorRest, ITestConnectorWs connectorWs, BalanceService balanceService) : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok(await balanceService.GetBalance());
    }
}