namespace SimplePollManager.Api.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            this.logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            var jsonErrorResponse = new ErrorResponse
            {
                Messages = new[] { "An internal error has occurred" }
            };

            if (this.env.IsDevelopment())
            {
                jsonErrorResponse.Exception = context.Exception.ToString();
            }

            context.Result = new ObjectResult(jsonErrorResponse) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }
    }
}
