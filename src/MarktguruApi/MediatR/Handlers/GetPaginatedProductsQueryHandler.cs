namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using global::MediatR;
    using JetBrains.Annotations;
    using Models.Product.Dtos;
    using Queries;
    using Repositories.Base.Interfaces;
    using Utils;

    [UsedImplicitly]
    internal sealed class GetPaginatedProductsQueryHandler(IProductRepository repository, IMapper mapper) 
        : IRequestHandler<GetPaginatedProductsQuery, PaginatedList<ProductReducedDto>>
    {
        public async Task<PaginatedList<ProductReducedDto>> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<ProductReducedDto> result = await repository.GetPaginatedAsync(mapper, request.Page, request.PageSize, cancellationToken);
            return result;
        }
    }
}