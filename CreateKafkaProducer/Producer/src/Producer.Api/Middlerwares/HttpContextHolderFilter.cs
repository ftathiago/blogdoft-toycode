using Microsoft.AspNetCore.Mvc.Filters;
using Producer.Api.Lib;
using System;
using System.Linq;

namespace Producer.Api.Middlerwares
{
    public class HttpContextHolderFilter : IActionFilter
    {
        private readonly HttpContextAccessor _holder;

        public HttpContextHolderFilter(HttpContextAccessor holder)
        {
            _holder = holder;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var correlationId = context.HttpContext.Request.Headers.FirstOrDefault(h => h.Key == "x-correlation-id").Value;
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            _holder.CorrelationId = Guid.Parse(correlationId);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Just for attend interface.
        }
    }
}
