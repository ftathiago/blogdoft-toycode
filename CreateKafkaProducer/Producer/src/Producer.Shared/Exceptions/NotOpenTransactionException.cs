using Producer.Shared.Extensions;
using System;

namespace Producer.Shared.Exceptions
{
    public class NotOpenTransactionException : UnitOfWorkException
    {
        private const string ErrorMessage =
            "There is no transaction openend to {0}";

        public NotOpenTransactionException()
        {
        }

        public NotOpenTransactionException(string operation)
            : base(ErrorMessage.Format(operation))
        {
        }

        public NotOpenTransactionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
