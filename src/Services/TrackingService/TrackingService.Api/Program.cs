using Infrastructure;
using Infrastructure.AutoMapper;
using Infrastructure.Redis;
using Infrastructure.SchemaRegistry;
using TrackingService.AppCore;
using TrackingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefault(builder.Configuration,
        [typeof(Program), typeof(Anchor), typeof(EventDispatcher)])
    .AddRedisService(builder.Configuration)
    .AddAutoMapperService(typeof(Anchor))
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumers(builder.Configuration)
    .AddMasstransitService(builder.Configuration);
var app = builder.Build();

app.UseServiceDefault();

app.Run();

