using Connector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureConnectorRestService();
var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
