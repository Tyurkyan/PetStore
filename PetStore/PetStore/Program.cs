using PetStore.ServiceExtensions;
using Mapster;
using PetStore.BL;
using PetStore.DL;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using PetStore.HealthCheck;
using FluentValidation.AspNetCore;
using FluentValidation;
using PetStore.Validations;
using PetStore.DL.Gateways;
using PetStore.DL.Interfaces;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:
        AnsiConsoleTheme.Code)
    .CreateLogger();


// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
     .AddDataDependencies(builder.Configuration)
    .AddBusinessDependencies();

builder.Services.AddMapster();

builder.Services.AddValidatorsFromAssemblyContaining<OwnerRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PetRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IPetBioGateway, PetBioGateway>();



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("Sample");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();