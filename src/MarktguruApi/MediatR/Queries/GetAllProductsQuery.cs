namespace MarktguruApi.MediatR.Queries
{
    using global::MediatR;
    using Models.Product.Dtos;

    public record GetAllProductsQuery : IRequest<List<ProductReducedDto>>;
}