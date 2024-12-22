using DriverService.AppCore;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)]);

var app = builder.Build();
app.UseServiceDefault();

app.Run();

