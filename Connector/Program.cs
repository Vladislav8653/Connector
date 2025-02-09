using Connector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureConnectorRestService();
builder.Services.SetupConfiguration(builder.Configuration);
var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
