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
    /// Handles the query to get all products.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="mapper">The mapper to convert between models and DTOs.</param>
    [UsedImplicitly]
    public sealed class GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        : IRequestHandler<GetAllProductsQuery, List<ProductReducedDto>>
    {
        /// <summary>
        /// Handles the request to get all products.
        /// </summary>
        /// <param name="request">The query request to get all products.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of reduced product DTOs.</returns>
        public async Task<List<ProductReducedDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> entries = await repository.GetAllAsync(cancellationToken);
            return mapper.Map<List<ProductReducedDto>>(entries);
        }
    }
}