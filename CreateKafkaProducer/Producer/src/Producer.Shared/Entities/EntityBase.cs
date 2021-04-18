using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Producer.Shared.Entities
{
    public abstract class EntityBase
    {
        public bool IsValid() => !GetValidations().Any();

        public IEnumerable<ValidationResult> GetValidations()
        {
            var validations = GetValidator();

            if (validations.IsValid)
            {
                yield break;
            }

            foreach (var error in validations.Errors)
            {
                yield return new ValidationResult(error.ErrorMessage);
            }
        }

        protected abstract FluentValidation.Results.ValidationResult GetValidator();
    }
}
