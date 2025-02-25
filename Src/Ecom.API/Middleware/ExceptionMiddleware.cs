using System.Net;
using System.Text.Json;
using Ecom.API.Errors;
using Microsoft.Extensions.Logging;

namespace Ecom.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment; // Check if the Project in Environment or Production Level
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                _logger.LogInformation("Success");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,$"This error come from exception middleware{ex.Message}");
                httpContext.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = _environment.IsDevelopment() ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(response, options);
                await httpContext.Response.WriteAsync(json);

                 _logger.LogError($"This error come from exception middleware{ ex.Message}");
            }
        }
    }
}
