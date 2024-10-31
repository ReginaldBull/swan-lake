namespace MarktguruApi.MediatR.Commands
{
    using global::MediatR;

    /// <summary>
    /// Command to delete a product by its ID.
    /// </summary>
    /// <param name="ProductId">The unique identifier of the product to be deleted.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    internal record DeleteProductCommand(Guid ProductId) : IRequest<bool>;
}