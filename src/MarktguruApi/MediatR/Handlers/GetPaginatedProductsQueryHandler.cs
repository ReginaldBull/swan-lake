namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using global::MediatR;
    using JetBrains.Annotations;
    using Models.Product.Dtos;
    using Queries;
    using Repositories.Base.Interfaces;
    using Utils;

    /// <summary>
    /// Handles the query to get paginated products.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="mapper">The mapper to convert between models and DTOs.</param>
    [UsedImplicitly]
    internal sealed class GetPaginatedProductsQueryHandler(IProductRepository repository, IMapper mapper)
        : IRequestHandler<GetPaginatedProductsQuery, PaginatedList<ProductReducedDto>>
    {
        /// <summary>
        /// Handles the request to get paginated products.
        /// </summary>
        /// <param name="request">The query request to get paginated products.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of reduced product DTOs.</returns>
        public async Task<PaginatedList<ProductReducedDto>> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<ProductReducedDto> result = await repository.GetPaginatedAsync(mapper, request.Page, request.PageSize, cancellationToken);
            return result;
        }
    }
}