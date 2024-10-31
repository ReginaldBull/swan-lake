namespace MarktguruApi.MediatR.Handlers
{
    using Commands;
    using global::MediatR;
    using JetBrains.Annotations;
    using Repositories.Base.Interfaces;

    /// <summary>
    /// Handles the command to delete a product by its ID.
    /// </summary>
    /// <param name="repository">The repository to interact with the product data store.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    [UsedImplicitly]
    internal sealed class DeleteProductCommandHandler(IProductRepository repository, ILogger<DeleteProductCommandHandler> logger) 
        : IRequestHandler<DeleteProductCommand, bool>
    {
        /// <summary>
        /// Handles the request to delete a product by its ID.
        /// </summary>
        /// <param name="request">The command request to delete the product.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Deleting product: {ProductId}", request.ProductId);
            return repository.DeleteAsync(request.ProductId, cancellationToken);
        }
    }
}