using System;
using System.Linq;
using System.Text;

namespace WebApi.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static StringBuilder GetAllMessage(this Exception exception, string separator, StringBuilder messages = null)
        {
            var stringBuilder = messages ?? new StringBuilder();

            stringBuilder
                .Append(exception.Message)
                .Append(separator);

            exception.InnerException?.GetAllMessage(separator, stringBuilder);

            if (exception is AggregateException agg)
            {
                agg.InnerExceptions
                    .ToList()
                    .ForEach(ex => ex.GetAllMessage(separator, stringBuilder));
            }

            return stringBuilder;
        }
    }
}
