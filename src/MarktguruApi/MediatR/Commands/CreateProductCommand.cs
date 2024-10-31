namespace MarktguruApi.MediatR.Commands
{
    using global::MediatR;
    using Models.Product.Dtos;

    public record CreateProductCommand(CreateProductDto CreateProductDto, Guid CorrelationId) : IRequest<ProductResponseDto>;
}