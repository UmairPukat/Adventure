using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CommonData.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception error)
            {
                var responce = context.Response;
                responce.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        //custom application error
                        responce.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        //not found error
                        responce.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case DbUpdateConcurrencyException e:
                        responce.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:
                        //unhandled error
                        _logger.LogError(error, error?.Message);
                        responce.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(error, error?.Message);
                var result = System.Text.Json.JsonSerializer.Serialize(new { message = error?.Message });
                await responce.WriteAsync(result);
            }
        }
    }
}
