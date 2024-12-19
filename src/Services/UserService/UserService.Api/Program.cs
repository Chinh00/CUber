using Infrastructure;
using Infrastructure.AutoMapper;
using Infrastructure.EfCore.Data;
using Infrastructure.EfCore.EventStore;
using UserService.AppCore;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Masstransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration, [typeof(Program), typeof(Anchor)])
    .AddEfCoreDefault<UserContext>(builder.Configuration, typeof(UserContext))
    .AddEventStore(builder.Configuration)
    .AddAutoMapperService(typeof(Anchor))
    .AddMasstransitService(builder.Configuration);

var app = builder.Build();

app.UseServiceDefault();

app.Run();
