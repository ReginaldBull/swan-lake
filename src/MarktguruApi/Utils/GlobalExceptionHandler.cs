namespace MarktguruApi.Utils
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Handles global exceptions in the application.
    /// </summary>
    /// <param name="logger">The logger instance for logging errors.</param>
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        /// <summary>
        /// Tries to handle the exception asynchronously.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the exception was handled.</returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Title = "Validation error";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Extensions.Add("errors", fluentException.Errors.Select(failure => failure.ErrorMessage).ToList());
            }
            else
            {
                problemDetails.Title = exception.Message;
            }

            logger.LogError("{problemDetailsTitle}", problemDetails.Title);

            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken)
                .ConfigureAwait(false);
            return true;
        }
    }
}