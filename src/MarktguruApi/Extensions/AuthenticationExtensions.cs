namespace MarktguruApi.Extensions
{
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Provides extension methods for adding authentication services.
    /// </summary>
    internal static class AuthenticationExtensions
    {
        /// <summary>
        /// Adds JWT authentication to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The configuration to use for JWT settings.</param>
        /// <returns>The IServiceCollection with the JWT authentication services added.</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey((Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }
    }
}