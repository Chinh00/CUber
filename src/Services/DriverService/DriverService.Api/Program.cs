using DriverService.AppCore;
using DriverService.Infrastructure;
using Infrastructure;
using Infrastructure.SchemaRegistry;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumers();

var app = builder.Build();
app.UseServiceDefault();

app.Run();

