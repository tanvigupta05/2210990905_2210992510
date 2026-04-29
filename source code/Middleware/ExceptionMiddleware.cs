using System.Net;
using System.Text.Json;

namespace LoggingObservabilityDemoAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var correlationId = context.Items["CorrelationId"]?.ToString();

                _logger.LogError(ex, "Error occurred | CorrelationId: {CorrelationId}", correlationId);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Something went wrong",
                    correlationId = correlationId
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}