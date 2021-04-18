using System;
using System.Runtime.Serialization;

namespace Producer.InfraData.Tests.Fixtures
{
    [Serializable]
    public class MockedException : Exception
    {
        public MockedException()
            : base(Fixture.Get().Lorem.Sentence())
        {
        }

        public MockedException(string message)
            : base(message)
        {
        }

        public MockedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MockedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}