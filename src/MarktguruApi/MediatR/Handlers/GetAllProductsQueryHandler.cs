namespace MarktguruApi.MediatR.Handlers
{
    using AutoMapper;
    using global::MediatR;
    using JetBrains.Annotations;
    using Models.Product;
    using Models.Product.Dtos;
    using Queries;
    using Repositories.Base.Interfaces;

    [UsedImplicitly]
    internal sealed class GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper) 
        : IRequestHandler<GetAllProductsQuery, List<ProductReducedDto>>
    {
        public async Task<List<ProductReducedDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> entries = await repository.GetAllAsync(cancellationToken);
            return mapper.Map<List<ProductReducedDto>>(entries);
        }
    }
}