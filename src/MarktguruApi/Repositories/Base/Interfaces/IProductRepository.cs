namespace MarktguruApi.Repositories.Base.Interfaces
{
    using AutoMapper;
    using Models.Product;
    using Models.Product.Dtos;
    using Utils;

    /// <summary>
    /// Interface for the product repository.
    /// </summary>
    public interface IProductRepository : IRepository<Product, CreateProductDto, UpdateProductDto>
    {
        /// <summary>
        /// Gets a paginated list of reduced product representations asynchronously.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping entities.</param>
        /// <param name="pageNumber">The number of the page to retrieve.</param>
        /// <param name="requestPageSize">The size of the page to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of reduced product representations.</returns>
        Task<PaginatedList<ProductReducedDto>> GetPaginatedAsync(IMapper mapper, int pageNumber, int requestPageSize, CancellationToken cancellationToken);
    }
}