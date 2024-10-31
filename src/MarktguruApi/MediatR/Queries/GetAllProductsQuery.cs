namespace MarktguruApi.MediatR.Queries
{
    using global::MediatR;
    using Models.Product.Dtos;

    /// <summary>
    /// Represents a query to retrieve all products.
    /// </summary>
    public record GetAllProductsQuery : IRequest<List<ProductReducedDto>>;
}