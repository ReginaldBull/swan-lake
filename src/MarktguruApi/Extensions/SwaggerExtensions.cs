namespace MarktguruApi.Extensions
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Provides extension methods for adding Swagger with authentication to the service collection.
    /// </summary>
    internal static class SwaggerExtensions
    {
        /// <summary>
        /// Adds Swagger with JWT authentication to the specified IServiceCollection.
        /// </summary>
        /// <param name="service">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection with the Swagger services added.</returns>
        public static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection service)
        {
            service.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter the JWT token into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
            return service;
        }
    }
}