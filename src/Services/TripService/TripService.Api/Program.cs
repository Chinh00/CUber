using Infrastructure;
using Infrastructure.SchemaRegistry;
using TripService.AppCore;
using TripService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddDataStore(builder.Configuration)
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumer();

var app = builder.Build();
app.UseServiceDefault();


app.Run();
