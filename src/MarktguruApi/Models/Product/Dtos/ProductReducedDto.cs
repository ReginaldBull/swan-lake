namespace MarktguruApi.Models.Product.Dtos
{
    public record ProductReducedDto(Guid Id, string Name, bool Available, decimal Price);
}