namespace MarktguruApi.Behaviors
{
    using FluentValidation;
    using FluentValidation.Results;
    using global::MediatR;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a validation behavior for MediatR pipeline.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    [UsedImplicitly]
    internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        /// <summary>
        /// Handles the validation of the request.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <param name="next">The next delegate in the pipeline.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
        /// <exception cref="ValidationException">Thrown when validation fails.</exception>
        public Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            List<ValidationFailure> failures = validators.Select(p => p.Validate(context))
                .SelectMany(p => p.Errors)
                .Where(p => p != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }

}