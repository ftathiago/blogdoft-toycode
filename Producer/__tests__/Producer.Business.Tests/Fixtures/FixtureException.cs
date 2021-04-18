using System;
using System.Runtime.Serialization;

namespace Producer.Business.Tests.Fixtures
{
    [Serializable]
    public class FixtureException : Exception
    {
        public FixtureException()
        {
        }
        public FixtureException(string message)
            : base(message)
        {
        }
        public FixtureException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected FixtureException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
