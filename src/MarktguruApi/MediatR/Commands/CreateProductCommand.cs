namespace MarktguruApi.MediatR.Commands
{
    using global::MediatR;
    using Models.Product.Dtos;

    /// <summary>
    /// Command to create a new product.
    /// </summary>
    /// <param name="CreateProductDto">The DTO containing the product creation details.</param>
    /// <param name="CorrelationId">The unique identifier for correlating the command.</param>
    /// <returns>A response DTO containing the created product details.</returns>
    public record CreateProductCommand(CreateProductDto CreateProductDto, Guid CorrelationId) : IRequest<ProductResponseDto>;
}