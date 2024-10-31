using FluentValidation;
using MarktguruApi;
using MarktguruApi.Behaviors;
using MarktguruApi.Extensions;
using MarktguruApi.Repositories.Base.Interfaces;
using MarktguruApi.Repositories.Product;
using MarktguruApi.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
    .Mapper()
    .AddResponseCaching()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
    .AddHealthChecks();

builder.Services.AddMediatR(p =>
{
    p.RegisterServicesFromAssemblyContaining<Program>();
    p.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
});

builder.Services.AddDbContextPool<ApplicationDbContext>(o =>
{
    o.UseInMemoryDatabase("ProductDb");
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddVersioningApiExplorer();

WebApplication app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/health");

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();