using DriverService.AppCore;
using DriverService.Infrastructure;
using DriverService.Infrastructure.Masstransit;
using Infrastructure;
using Infrastructure.EfCore.EventStore;
using Infrastructure.Mongodb;
using Infrastructure.SchemaRegistry;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor), typeof(EventDispatcher)])
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumers()
    .AddEventStore(builder.Configuration)
    .AddDataStore(builder.Configuration)
    .AddMongodbService(builder.Configuration)
    .AddMasstransitService(builder.Configuration);

var app = builder.Build();
app.UseServiceDefault();

app.Run();

