using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ZestTechnicalAssisgnMent.Api.ExceptionHandler
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) :IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError($"An error occurred while processing your request: {exception.Message}");
            ProblemDetails problemDetails = new()
            {
                Status = GetStatusCode(exception),
                Type = exception.GetType().Name,
                Title = "An unhandled error occurred",
                Detail = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
