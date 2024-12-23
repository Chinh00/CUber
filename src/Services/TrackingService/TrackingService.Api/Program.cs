using Infrastructure;
using Infrastructure.AutoMapper;
using Infrastructure.Redis;
using Infrastructure.SignaIR;
using TrackingService.Api.Hubs;
using TrackingService.AppCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddSocketService()
    .AddRedisService(builder.Configuration)
    .AddAutoMapperService(typeof(Anchor));
var app = builder.Build();

app.UseServiceDefault();
app.UseSocketService<TrackingHub>();

app.Run();

