namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using global::MediatR;
    using JetBrains.Annotations;
    using Commands;
    using Models.Product;
    using Models.Product.Dtos;
    using Models.Results;
    using Repositories.Base.Interfaces;

    /// <summary>
    /// Handles the creation of a product.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="mapper">The mapper to convert between models and DTOs.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    [UsedImplicitly]
    public sealed class CreateProductCommandHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<CreateProductCommandHandler> logger) : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        /// <summary>
        /// Handles the creation of a product.
        /// </summary>
        /// <param name="request">The command containing the product creation details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The created product response DTO.</returns>
        /// <exception cref="Exception">Thrown when the product creation fails.</exception>
        public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Creating product: {request}", request);
            CreateResult<Product?> result = await repository.CreateAsync(request.CreateProductDto, cancellationToken);
            if (result.Success)
            {
                logger.LogDebug("Created product: {0}", result.Entry);
                return mapper.Map<ProductResponseDto>(result.Entry);
            }
            logger.LogError("Failed to create product: {ErrorMessage}", result.ErrorMessage);
            throw new Exception("Failed to create product");
        }
    }
}