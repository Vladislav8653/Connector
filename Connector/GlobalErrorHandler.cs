using System.Net;
using System.Text.Json;

namespace Connector;

public class GlobalErrorHandler
{
    private readonly RequestDelegate _next;

    private class ExceptionDetails
    {
        public ExceptionDetails()
        {
            Type = Message = string.Empty;
        }
        public string Type { get; set; }
        public string Message { get; set; }
    }
    
    public GlobalErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var exceptionDetails = new ExceptionDetails
            {
                Message = error.Message, // сообщение исключения
                Type = error.GetType().Name, // название исключения
            };
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                _ => (int)HttpStatusCode.InternalServerError
            };
            var result = JsonSerializer.Serialize(exceptionDetails);
            await response.WriteAsync(result);
        }
    }
}