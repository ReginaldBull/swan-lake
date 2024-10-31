namespace MarktguruApi.MediatR.Queries
{
    using global::MediatR;
    using Models.Product.Dtos;

    /// <summary>
    /// Query to get a product by its ID.
    /// </summary>
    /// <param name="ProductId">The ID of the product to retrieve.</param>
    /// <returns>A <see cref="ProductResponseDto"/> containing the product details.</returns>
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponseDto>;
}