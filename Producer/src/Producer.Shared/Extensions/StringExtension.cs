using System.Diagnostics.CodeAnalysis;

namespace Producer.Shared.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class StringExtension
    {
        public static string Format(this string format, params object[] args) =>
            string.Format(format, args);
    }
}