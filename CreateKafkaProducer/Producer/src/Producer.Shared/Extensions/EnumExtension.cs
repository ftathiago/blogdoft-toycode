using System;

namespace Producer.Shared.Extensions
{
    public static class EnumExtension
    {
        public static int AsInteger(this Enum value) =>
            Convert.ToInt32(value);
    }
}