using Infrastructure.SchemaRegistry;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddDataStore(builder.Configuration)
    .AddMasstransitService(builder.Configuration)
    //.AddMongodbService(builder.Configuration)
    .AddSchemaRegistryService(builder.Configuration);


var app = builder.Build();

app.UseServiceDefault();

app.Run();
