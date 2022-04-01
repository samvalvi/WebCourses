using App.ErrorHandler;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            object errors = null;
            switch(ex)
            {
                case Error e :
                    logger.LogError(ex, e.Message);
                    errors = e.ErrorMessage;
                    context.Response.StatusCode = (int)e.StatusCode;
                    break;
                case Exception e:
                    logger.LogError(ex, e.Message);
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if(errors != null)
            {
                var results = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(results);
            }
        }
    }
}
