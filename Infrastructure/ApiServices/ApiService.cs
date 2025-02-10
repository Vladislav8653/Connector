﻿using Application.Contracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Infrastructure.ApiServices;

public class ApiService : IApiService
{
    private readonly ExchangeApiSettings _config;

    public ApiService(IOptions<ExchangeApiSettings> settings)
    {
        _config = settings.Value;
    }

    public async Task<string?> GetTradesData(string pair, int maxCount, int? sort, long? start, long? end)
    {
        var baseUrl = $"{_config.BaseUrl}{_config.Version}{_config.Endpoints.Trades}/t{pair}/hist";
        var parameters = new Dictionary<string, object?>
        {
            { "limit", maxCount },
            { "sort", sort },
            { "start", start },
            { "end", end }
        };
        var url = UriParamsBuilder.BuildUri(baseUrl, parameters);
        var options = new RestClientOptions(url);
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);
        return response.Content;
    }

    public async Task<string?> GetCandleSeries(string pair, string timeFrame, DateTimeOffset? from,
        DateTimeOffset? to = null, long? count = 0, int? sort = null)
    {
        var baseUrl = $"{_config.BaseUrl}{_config.Version}{_config.Endpoints.Candles}/trade:{timeFrame}:t{pair}/hist";
        var parameters = new Dictionary<string, object?>
        {
            { "limit", count },
            { "sort", sort },
            { "start", from?.ToUnixTimeMilliseconds() },
            { "end", to?.ToUnixTimeMilliseconds() }
        };
        var url = UriParamsBuilder.BuildUri(baseUrl, parameters);
        var options = new RestClientOptions(url);
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);
        return response.Content;

    }
}