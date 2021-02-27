using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebApi.InfraData.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class InfraDataException : Exception
    {
        public InfraDataException()
        {
        }

        public InfraDataException(string message)
            : base(message)
        {
        }

        public InfraDataException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InfraDataException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}