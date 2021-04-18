using System;
using System.Runtime.Serialization;

namespace Producer.Shared.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        protected CustomException()
        {
        }

        protected CustomException(string message)
            : base(message)
        {
        }

        protected CustomException(string message, string detail)
            : base(message)
        {
            Detail = detail;
        }

        protected CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CustomException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public string Detail { get; init; }
    }
}
