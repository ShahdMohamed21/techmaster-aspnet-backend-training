using System.Text.Json;
using TrainngCenter.Api.Common.Responses;

namespace TrainngCenter.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException)
                when (context.RequestAborted.IsCancellationRequested)
            {

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception. TraceId: {TraceId}, Path: {Path}",
                    context.TraceIdentifier,
                    context.Request.Path);

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.FailureResponse(
                    "An unexpected error occurred.",
                    new List<string>
                    {
                        "Please try again later.",
                        $"Trace ID: {context.TraceIdentifier}"
                    });

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(
                    json,
                    context.RequestAborted);
            }
        }
    }
}
