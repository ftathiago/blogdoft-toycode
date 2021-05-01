using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebApi.Shared.Extensions;

namespace WebApi.Api.Filters
{
    public class ControllerExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ControllerExceptionFilter> _logger;

        public ControllerExceptionFilter(ILogger<ControllerExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception
                    .GetAllMessage(",")
                    .ToString();

            _logger.LogError(context.Exception, errorMessage);

            context.ExceptionHandled = true;

            context.Result = new BadRequestObjectResult(new
            {
                errorMessage,
            });
        }
    }
}
