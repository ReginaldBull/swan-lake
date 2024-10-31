namespace MarktguruApi.Extensions
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    internal static class VersioningExtensions
    {
        public static IServiceCollection AddApiExplorer(this IServiceCollection services)
        {
            IApiVersioningBuilder apiVersioningBuilder = services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            apiVersioningBuilder.AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}