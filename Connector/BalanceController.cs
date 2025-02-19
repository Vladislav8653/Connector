using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"balance")]
public class BalanceController(BalanceService balanceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBalance()
    {
        return Ok(await balanceService.GetBalance());
    }
}