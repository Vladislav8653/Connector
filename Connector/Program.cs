using Connector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureConnectorService();
builder.Services.ConfigureExternalApiService();
builder.Services.SetupConfiguration(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandler>();
app.UseRouting();
app.MapControllers();
app.Run();
