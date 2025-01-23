
using PetStore.ServiceExtensions;
using Mapster;
using PetStore.BL;
using PetStore.DL;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:
        AnsiConsoleTheme.Code)
    .CreateLogger();


// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
    .AddDataDependencies()
    .AddBusinessDependencies();

builder.Services.AddMapster();




builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();