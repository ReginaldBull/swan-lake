namespace MarktguruApi.Behaviors
{
    using System.Text.Json;
    using global::MediatR;

    /// <summary>
    /// Represents a logging behavior for MediatR pipeline to log requests and responses.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    internal class RequestResponseLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestResponseLoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging requests and responses.</param>
        public RequestResponseLoggingBehavior(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Handles the logging of the request and response.
        /// </summary>
        /// <param name="request">The request to log.</param>
        /// <param name="next">The next delegate in the pipeline.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();

            string requestJson = JsonSerializer.Serialize(request);

            logger.LogInformation("Handling request {correlationId}: {request}", correlationId, requestJson);

            TResponse response = await next();

            string responseJson = JsonSerializer.Serialize(response);

            logger.LogInformation("Response for {correlationId}: {response}", correlationId, responseJson);

            return response;
        }
    }
}