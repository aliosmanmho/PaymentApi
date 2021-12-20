using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Extensions
{
    public static class Converter
    {
        public static T ToEnum<T>(this int value)
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException($"{type} is not an enum.");

            }

            if (!type.IsEnumDefined(value))
            {
                throw new ArgumentException($"{value} is not a valid ordinal of type {type}.");
            }

            return (T)Enum.ToObject(type, value);
        }
        public static T TryCast<T>(this object input)
        {
            bool success;
            return TryCast<T>(input, out success);
        }
        public static T TryCast<T>(this object input, out bool success)
        {
            success = true;
            if (input is T)
                return (T)input;
            success = false;
            return default(T);
        }
    }
}
