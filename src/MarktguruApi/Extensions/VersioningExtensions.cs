namespace MarktguruApi.Extensions
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides extension methods for adding API versioning and API explorer services.
    /// </summary>
    internal static class VersioningExtensions
    {
        /// <summary>
        /// Adds API versioning and API explorer services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection with the API versioning and API explorer services added.</returns>
        public static IServiceCollection AddVersioningApiExplorer(this IServiceCollection services)
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