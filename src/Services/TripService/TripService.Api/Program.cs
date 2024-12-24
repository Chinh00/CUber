using Infrastructure;
using Infrastructure.SchemaRegistry;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TripService.AppCore;
using TripService.Infrastructure;
using TripService.Infrastructure.Masstransit;

var builder = WebApplication.CreateBuilder(args);
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddDataStore(builder.Configuration)
    .AddSchemaRegistryService(builder.Configuration)
    .AddCdcConsumer()
    .AddMasstransitService(builder.Configuration);

var app = builder.Build();
app.UseServiceDefault();


app.Run();
