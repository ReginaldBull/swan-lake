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
    public sealed class GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? entry = await repository.GetByIdAsync(request.ProductId, cancellationToken);
            return mapper.Map<ProductResponseDto>(entry);
        }
    }
} 