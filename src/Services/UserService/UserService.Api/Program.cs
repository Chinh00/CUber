using Infrastructure.SchemaRegistry;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddEfCoreDefault<UserContext>(builder.Configuration, typeof(UserContext))
    .AddEventStore(builder.Configuration)
    .AddAutoMapperService(typeof(Anchor))
    .AddMasstransitService(builder.Configuration)
    .AddMongodbService(builder.Configuration)
    .AddSchemaRegistryService(builder.Configuration);

var app = builder.Build();

app.UseServiceDefault();

app.Run();
