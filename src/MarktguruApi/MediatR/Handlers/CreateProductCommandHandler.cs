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

    [UsedImplicitly]
    public sealed class CreateProductCommandHandler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<CreateProductCommandHandler> logger) : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
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