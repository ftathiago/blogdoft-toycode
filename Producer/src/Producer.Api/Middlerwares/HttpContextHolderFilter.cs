using Microsoft.AspNetCore.Mvc.Filters;
using Producer.Shared.Holders;
using System;
using System.Linq;

namespace Producer.Api.Middlerwares
{
    public class HttpContextHolderFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _holder;

        public HttpContextHolderFilter(IHttpContextAccessor holder)
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
