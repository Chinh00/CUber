using Infrastructure;
using Infrastructure.EfCore.Data;
using Infrastructure.SchemaRegistry;
using TripService.Infrastructure;
using TripService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program)])
    .AddEfCoreDefault<TripContext>(builder.Configuration, typeof(TripContext))
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumer();

var app = builder.Build();
app.UseServiceDefault();


app.Run();
