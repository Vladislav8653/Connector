using Connector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureConnectorServices();
builder.Services.ConfigureExternalApiServices();
builder.Services.ConfigureBusinessServices();
builder.Services.SetupConfiguration(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandler>();
app.UseRouting();
app.MapControllers();
app.Run();
