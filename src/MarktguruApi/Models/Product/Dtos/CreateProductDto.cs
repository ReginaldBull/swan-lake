namespace MarktguruApi.Models.Product.Dtos
{
    public record CreateProductDto(string Name, decimal Price, bool Available, string Description);
}