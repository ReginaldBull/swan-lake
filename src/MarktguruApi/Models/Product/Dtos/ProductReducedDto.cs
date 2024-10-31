namespace MarktguruApi.Models.Product.Dtos
{
    /// <summary>
    /// Data Transfer Object for a reduced product representation.
    /// </summary>
    /// <param name="Id">The unique identifier of the product.</param>
    /// <param name="Name">The name of the product.</param>
    /// <param name="Available">Indicates whether the product is available.</param>
    /// <param name="Price">The price of the product.</param>
    public record ProductReducedDto(Guid Id, string Name, bool Available, decimal Price);
}