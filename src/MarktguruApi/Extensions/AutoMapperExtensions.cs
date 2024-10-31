namespace MarktguruApi.Extensions
{
    using Models.Product;
    using Models.Product.Dtos;

    internal static class AutoMapperExtensions
    {
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