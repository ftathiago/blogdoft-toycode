using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Producer.Shared.Extensions
{
    public static class ValidationResultExtension
    {
        public static string GetStrings(this IEnumerable<ValidationResult> validations) =>
            validations.Aggregate(
                new StringBuilder(),
                (sb, error) => sb.AppendLine(error.ErrorMessage))
                .ToString();
    }
}
