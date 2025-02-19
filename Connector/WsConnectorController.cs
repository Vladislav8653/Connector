using Application.Contracts;
using Application.DTO.WSConnector;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"ws")]
public class WsConnectorController(ITestConnectorWs connectorWs) : ControllerBase
{
 
    [HttpPost]
    [Route("trades/subscribe")]
    public async Task<IActionResult> SubscribeTrades([FromBody] PairDto pairDto)
    {
        await connectorWs.SubscribeTradesAsync(pairDto.Pair);
        return Ok("Subscribed to Trades");
    }

    [HttpPost]
    [Route("trades/unsubscribe")]
    public async Task<IActionResult> UnsubscribeTrades([FromBody] PairDto pairDto)
    {
        await connectorWs.UnsubscribeTradesAsync(pairDto.Pair);
        return Ok("Unsubscribed from Trades");
    }
    
    [HttpPost]
    [Route("candles/subscribe")]
    public async Task<IActionResult> SubscribeCandles([FromBody] SubCandlesDto candlesDto)
    {
        await connectorWs.SubscribeCandlesAsync(candlesDto.Pair, candlesDto.TimeFrame);
        return Ok();
    }

    [HttpPost]
    [Route("candles/unsubscribe")]
    public async Task<IActionResult> UnsubscribeCandles([FromBody] PairDto pairDto)
    {
        await connectorWs.UnsubscribeCandlesAsync(pairDto.Pair);
        return Ok();
    }
}