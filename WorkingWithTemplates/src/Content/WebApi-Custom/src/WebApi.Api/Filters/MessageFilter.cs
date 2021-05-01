using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Shared.Holders;

namespace WebApi.Api.Filters
{
    public class MessageFilter : IActionFilter
    {
        private readonly IMessageHolder _messageHolder;

        public MessageFilter(IMessageHolder messageHolder)
        {
            _messageHolder = messageHolder;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!_messageHolder.Any())
            {
                return;
            }

            context.HttpContext.Response.StatusCode = (int)_messageHolder.StatusCode;
            context.Result = new ObjectResult(new
            {
                errorMessage = _messageHolder.StringifyMessages(),
                originalData = context.Result,
            });
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do nothing
        }
    }
}
