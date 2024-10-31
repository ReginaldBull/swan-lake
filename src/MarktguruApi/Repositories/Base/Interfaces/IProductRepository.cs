namespace MarktguruApi.Repositories.Base.Interfaces
{
    using AutoMapper;
    using Models.Product;
    using Models.Product.Dtos;
    using Utils;

    public interface IProductRepository : IRepository<Product, CreateProductDto>
    {
        Task<PaginatedList<ProductReducedDto>> GetPaginatedAsync(IMapper mapper, int pageNumber, int requestPageSize, CancellationToken cancellationToken);
    }
}