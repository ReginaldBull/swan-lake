namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using Commands;
    using global::MediatR;
    using JetBrains.Annotations;
    using Models.Product;
    using Models.Product.Dtos;
    using Models.Results;
    using Repositories.Base.Interfaces;

    /// <summary>
    /// Handles the command to update an existing product.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="mapper">The mapper to convert between models and DTOs.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    [UsedImplicitly]
    internal sealed class UpdateProductCommandHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<UpdateProductCommandHandler> logger)
        : IRequestHandler<UpdateProductCommand, ProductResponseDto>
    {
        /// <summary>
        /// Handles the request to update an existing product.
        /// </summary>
        /// <param name="request">The command request to update the product.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated product response DTO.</returns>
        public async Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Updating product: {ProductId}", request.ProductId);
            UpdateResult<Product> result = await repository.UpdateAsync(request.ProductId, request.UpdateProductDto, cancellationToken);
            if (result.Success)
            {
                logger.LogDebug("Updated product: {ProductId}", request.ProductId);
                return mapper.Map<ProductResponseDto>(result.Entry);
            }
            logger.LogError("Failed to update product: {ErrorMessage}", result.ErrorMessage);
            throw new Exception("Failed to update product");
        }
    }
}