namespace MarktguruApi.MediatR.Queries
{
    using global::MediatR;
    using Models.Product.Dtos;
    using Utils;

    /// <summary>
    /// Represents a query to retrieve a paginated list of products.
    /// </summary>
    /// <param name="Page">The page number to retrieve.</param>
    /// <param name="PageSize">The number of items per page.</param>
    /// <returns>A paginated list of products.</returns>
    public record GetPaginatedProductsQuery(int Page, int PageSize) : IRequest<PaginatedList<ProductReducedDto>>;
}