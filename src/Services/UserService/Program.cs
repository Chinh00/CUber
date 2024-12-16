using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefault(builder.Configuration);
var app = builder.Build();

app.UseServiceDefault();

app.Run();
