namespace MarktguruApi.Models.Product.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating a product.
    /// </summary>
    /// <param name="Name">The name of the product.</param>
    /// <param name="Price">The price of the product.</param>
    /// <param name="Available">Indicates whether the product is available.</param>
    /// <param name="Description">The description of the product.</param>
    /// <param name="Version">The version of the product.</param>
    public record UpdateProductDto(string Name, decimal Price, bool Available, string Description, int Version);
}