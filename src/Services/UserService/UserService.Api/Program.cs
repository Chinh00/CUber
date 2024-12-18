using Infrastructure;
using Infrastructure.EfCore.Data;
using UserService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration,[typeof(Program)])
    .AddEfCoreDefault<UserContext>(builder.Configuration);

var app = builder.Build();

app.UseServiceDefault();

app.Run();
