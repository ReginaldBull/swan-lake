namespace MarktguruApi.Models.Product.Dtos
{
    public record UpdateProductDto(string Name, decimal Price, bool Available, string Description, int Version);
}