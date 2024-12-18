using Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefault(builder.Configuration,[typeof(Program)]);
var app = builder.Build();

app.UseServiceDefault();

app.Run();
