using Application.Contracts;
using Application.Services;
using Moq;
using Xunit;

namespace Tests;

public class TestConnectorRestTests
{
    private readonly Mock<IApiServiceRest> _apiServiceRestMock;
    private readonly TestConnectorRest _testConnectorRest;

    public TestConnectorRestTests()
    {
        _apiServiceRestMock = new Mock<IApiServiceRest>();
        _testConnectorRest = new TestConnectorRest(_apiServiceRestMock.Object);
    }

    [Fact]
    public async Task GetNewTradesAsync_ReturnsTrades_WhenValidDataIsProvided()
    {
        // Arrange
        var pair = "BTCUSD";
        var maxCount = 10;
        var responseData = "1,1628900000000,0.1,30000\n2,1628900000001,0.2,31000"; // Пример данных
        _apiServiceRestMock.Setup(api => api.GetTradesDataAsync(pair, maxCount, null, null, null))
            .ReturnsAsync(responseData);

        // Act
        var trades = await _testConnectorRest.GetNewTradesAsync(pair, maxCount);

        // Assert
        Assert.NotNull(trades);
        Assert.Equal(2, trades.Count());
        Assert.Equal("BTCUSD", trades.First().Pair);
        Assert.Equal(30000, trades.First().Price);
        Assert.Equal(0.1m, trades.First().Amount);
    }

    [Fact]
    public async Task GetCandleSeriesAsync_ReturnsCandles_WhenValidDataIsProvided()
    {
        // Arrange
        var pair = "BTC/USD";
        var timeFrame = "1h";
        var responseData = "1628900000000,30000,31000,29000,30500,100"; // Пример данных
        _apiServiceRestMock.Setup(api => api.GetCandleSeriesDataAsync(pair, timeFrame, null, null, null, null))
            .ReturnsAsync(responseData);

        // Act
        var candles = await _testConnectorRest.GetCandleSeriesAsync(pair, timeFrame, null);

        // Assert
        Assert.NotNull(candles);
        Assert.Single(candles);
        var candle = candles.First();
        Assert.Equal("BTC/USD", candle.Pair);
        Assert.Equal(30000, candle.OpenPrice);
        Assert.Equal(31000, candle.HighPrice);
        Assert.Equal(29000, candle.LowPrice);
        Assert.Equal(30500, candle.ClosePrice);
    }

    [Fact]
    public async Task GetTickerAsync_ReturnsTicker_WhenValidDataIsProvided()
    {
        // Arrange
        var pair = "BTC/USD";
        var responseData = "30000,0.5,31000,0.3,100,0.1,30500,1000,32000,29000"; // Пример данных
        _apiServiceRestMock.Setup(api => api.GetTickerDataAsync(pair))
            .ReturnsAsync(responseData);

        // Act
        var ticker = await _testConnectorRest.GetTickerAsync(pair);

        // Assert
        Assert.NotNull(ticker);
        Assert.Equal("BTC/USD", ticker.Pair);
        Assert.Equal(30000, ticker.Bid);
        Assert.Equal(31000, ticker.Ask);
        Assert.Equal(30500, ticker.LastPrice);
    }
}