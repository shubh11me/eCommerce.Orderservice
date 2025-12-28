namespace eCommerce.Orderservice.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next,ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                return _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = new { message = ex.Message, type = ex.GetType().ToString() };
                _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                if(ex.InnerException != null)
                {
                    _logger.LogError("Inner Exception of {type} : {InnerMessage}",ex.GetType().ToString(), ex.InnerException.Message);
                }
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                return httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
