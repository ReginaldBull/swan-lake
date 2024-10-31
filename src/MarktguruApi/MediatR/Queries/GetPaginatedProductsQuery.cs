namespace MarktguruApi.MediatR.Queries
{
    using global::MediatR;
    using Models.Product.Dtos;
    using Utils;

    public record GetPaginatedProductsQuery(int Page, int PageSize) : IRequest<PaginatedList<ProductReducedDto>>;
}