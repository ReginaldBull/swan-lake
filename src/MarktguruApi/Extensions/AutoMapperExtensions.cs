namespace MarktguruApi.Extensions
{
    using Models.Product;
    using Models.Product.Dtos;

    /// <summary>
    /// Provides extension methods for AutoMapper configuration.
    /// </summary>
    internal static class AutoMapperExtensions
    {
        /// <summary>
        /// Adds AutoMapper services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The IServiceCollection with the AutoMapper services added.</returns>
        public static IServiceCollection Mapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<CreateProductDto, Product>();
                cfg.CreateMap<Product, ProductResponseDto>();
                cfg.CreateMap<Product, ProductReducedDto>();
            });
            return services;
        }
    }
}