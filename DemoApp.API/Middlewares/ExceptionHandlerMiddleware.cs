using System.Net;

namespace DemoApp.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Logging
                var errorId = Guid.NewGuid().ToString();
                _logger.LogError(ex, $"{errorId} : {ex.Message} ");

                // Custom reponse back
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong!!"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}