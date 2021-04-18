using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Producer.Shared.Exceptions;
using Producer.Shared.Extensions;

namespace Producer.Api.Middlerwares
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
            context.ExceptionHandled = true;

            var problemDetail = CreateErrorMessage(context.Exception);

            _logger.LogError(context.Exception, problemDetail.Title);

            context.Result = new BadRequestObjectResult(problemDetail);
        }

        public ProblemDetails CreateErrorMessage(Exception exception)
        {
            var problemDetail = new ProblemDetails
            {
                Title = exception
                    .GetAllMessage(",")
                    .ToString(),
            };

            if (exception is CustomException custom)
            {
                problemDetail.Detail = custom.Detail;
            }

            return problemDetail;
        }
    }
}
