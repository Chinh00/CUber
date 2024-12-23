using DriverService.AppCore;
using DriverService.Infrastructure;
using Infrastructure;
using Infrastructure.EfCore.EventStore;
using Infrastructure.Mongodb;
using Infrastructure.SchemaRegistry;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddSchemaRegistryService(builder.Configuration)
    .AddDataStore(builder.Configuration)
    .AddCdcConsumers()
    .AddEventStore(builder.Configuration)
    .AddMongodbService(builder.Configuration);

var app = builder.Build();
app.UseServiceDefault();

app.Run();

