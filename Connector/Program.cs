using Connector;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Connector", Version = "v1" });
});
builder.Services.ConfigureConnectorServices();
builder.Services.ConfigureExternalApiServices();
builder.Services.ConfigureBusinessServices();
builder.Services.SetupConfiguration(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandler>();
app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Connector");
    c.RoutePrefix = string.Empty; 
});
app.Run();
