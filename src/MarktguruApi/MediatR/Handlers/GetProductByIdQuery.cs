namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using global::MediatR;
    using JetBrains.Annotations;
    using Models.Product;
    using Models.Product.Dtos;
    using Queries;
    using Repositories.Base.Interfaces;

    /// <summary>
    /// Handles the query to get a product by its ID.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="mapper">The mapper to convert between models and DTOs.</param>
    [UsedImplicitly]
    public sealed class GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        /// <summary>
        /// Handles the request to get a product by its ID.
        /// </summary>
        /// <param name="request">The query request to get the product by ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product response DTO.</returns>
        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? entry = await repository.GetByIdAsync(request.ProductId, cancellationToken);
            return mapper.Map<ProductResponseDto>(entry);
        }
    }
}