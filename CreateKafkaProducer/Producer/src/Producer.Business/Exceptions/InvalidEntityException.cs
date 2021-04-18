using Producer.Shared.Exceptions;
using Producer.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Producer.Business.Exceptions
{
    [Serializable]
    public class InvalidEntityException : CustomException
    {
        public InvalidEntityException(
            string entityName,
            IEnumerable<ValidationResult> validationResult)
            : base(
                message: "Entity {0} is invalid.".Format(entityName),
                detail: validationResult.GetStrings())
        {
        }

        protected InvalidEntityException()
            : base(message: "Invalid entity.")
        {
        }

        protected InvalidEntityException(string message)
            : base(message)
        {
        }

        protected InvalidEntityException(string message, string detail)
            : base(message, detail)
        {
        }

        protected InvalidEntityException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidEntityException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
