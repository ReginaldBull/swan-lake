namespace MarktguruApi.MediatR.Commands
{
    using global::MediatR;
    using Models.Product.Dtos;

    /// <summary>
    /// Command to update an existing product.
    /// </summary>
    /// <param name="ProductId">The unique identifier of the product to be updated.</param>
    /// <param name="UpdateProductDto">The DTO containing the product update details.</param>
    /// <returns>A response DTO containing the updated product details.</returns>
    internal record UpdateProductCommand(Guid ProductId, UpdateProductDto UpdateProductDto) : IRequest<ProductResponseDto>;
}