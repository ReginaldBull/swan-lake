using FluentValidation;
using MarktguruApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddJwtAuthentication(configuration)
    .AddAuthorization()
    .AddEndpointsApiExplorer()
    .AddSwaggerWithAuthentication()
    .AddResponseCaching()
    .AddProblemDetails()
    .AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();