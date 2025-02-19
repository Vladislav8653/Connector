using Application.Contracts;
using Application.DTO;
using Application.DTO.RestConnector;
using Microsoft.AspNetCore.Mvc;

namespace Connector;

[ApiController]
[Route($"connector")]
public class RestConnectorController(ITestConnectorRest connectorRest) : ControllerBase
{
    
    [HttpPost]
    [Route("trades")]
    public async Task<IActionResult> GetNewTrades([FromBody] TradesDto tradesDto)
    {
        return Ok(await connectorRest.GetNewTradesAsync(tradesDto.Pair, tradesDto.MaxCount,
            tradesDto.Sort, tradesDto.Start, tradesDto.End));
    }
    
    [HttpPost]
    [Route("candles")]
    public async Task<IActionResult> GetCandleSeries([FromBody] CandlesDto candlesDto)
    {
        return Ok(await connectorRest.GetCandleSeriesAsync(candlesDto.Pair, candlesDto.TimeFrame, 
            candlesDto.From, candlesDto.To, candlesDto.Count, candlesDto.Sort));
    }
    
    [HttpPost]
    [Route("ticker")]
    public async Task<IActionResult> GetTicker(PairDto pairDto)
    {
        await connectorRest.GetTickerAsync(pairDto.Pair);
        return Ok();
    }
}