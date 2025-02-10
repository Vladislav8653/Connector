namespace Infrastructure.Utilities;

public static class UriParamsBuilder
{
    // Универсальный метод для добавления параметров в URL 
    public static Uri BuildUri(string baseUrl, Dictionary<string, object?> parameters)
    {
        var uriBuilder = new UriBuilder(baseUrl);
        var query = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.Value != null)
            {
                query.Add($"{parameter.Key}={parameter.Value}");
            }
        }
        uriBuilder.Query = string.Join("&", query);
        return uriBuilder.Uri;
    }
}