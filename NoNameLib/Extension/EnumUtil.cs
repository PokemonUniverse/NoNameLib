using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using NoNameLib.Attributes;

namespace NoNameLib.Extension
{
    public static class EnumUtil
    {
        /// <summary>
        /// Will get the string value for a given Enum value. This will only work if you assign the StringValue attribute to the items in your enumeration.
        /// </summary>
        /// <param name="value">The Enum value to get the StringValue from.</param>
        /// <returns>
        /// The value of the StringValue attribute; otherwise String.Empty.
        /// </returns>
        public static string GetStringValue(this Enum value)
        {
            return GetStringValue(value, null);
        }

        /// <summary>
        /// Will get the string value for a given Enum value. This will only work if you assign the StringValue attribute to the items in your enumeration.
        /// </summary>
        /// <param name="value">The Enum value to get the StringValue from.</param>
        /// <param name="key">The case-insensitive Key of the StringValueAttribute to get the StringValue from.</param>
        /// <returns>
        /// The value of the StringValue attribute; otherwise String.Empty.
        /// </returns>
        public static string GetStringValue(this Enum value, string key)
        {
            string stringValue = String.Empty;

            // Get the Type of the Enum
            Type enumType = value.GetType();

            // Get FieldInfo for the Type
            FieldInfo fieldInfo = enumType.GetField(value.ToString());

            // Get the StringValueAttributes
            var stringValueAttributes = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (stringValueAttributes != null)
            {
                // Get the StringValue from the StringValueAttributes matching the key
                foreach (var stringValueAttribute in stringValueAttributes)
                {
                    if (String.Equals(stringValueAttribute.Key, key, StringComparison.OrdinalIgnoreCase))
                    {
                        stringValue = stringValueAttribute.StringValue;
                        break;
                    }
                }
            }

            // Fall back to name of Enum
            if (stringValue.IsNullOrWhiteSpace())
                stringValue = value.ToString();

            return stringValue;
        }

        /// <summary>
        /// Convert an Enum to it's int representation.
        /// </summary>
        /// <param name="value">The Enum value to get the int value from.</param>
        /// <returns>The value as an int.</returns>
        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Convert an Enum to an integer String value.
        /// </summary>
        /// <param name="value">The Enum value to get the integer String value from.</param>
        /// <returns></returns>
        public static string ToIntString(this Enum value)
        {
            return Convert.ToInt32(value).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retrieve a typed Enum from a String value.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse to.</typeparam>
        /// <param name="value">The String value.</param>
        /// <returns>Typed Enum instance for the specified String value.</returns>
        public static T ToEnum<T>(this string value)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return ToEnum<T>(value, false);
        }

        /// <summary>
        /// Retrieve a typed Enum from a String value.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse to.</typeparam>
        /// <param name="value">The String value.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <returns>Typed Enum instance for the specified String value.</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Can't parse an empty string", "value");
            }
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an enumerated type.");
            }

            // Convert value to PascalCase
            value = value.ToPascalCase();

            // Warning, can throw ArgumentExceptions
            return (T)Enum.Parse(enumType, value, ignoreCase);
        }

        /// <summary>
        /// Retrieve a typed Enum from an int value.
        /// </summary>
        /// <typeparam name="T">The Enum type to parse to.</typeparam>
        /// <param name="value">The int value.</param>
        /// <returns>Typed Enum instance for the specified int value.</returns>
        public static T ToEnum<T>(this int value)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an enumerated type.");
            }

            // Warning, can throw ArgumentExceptions
            return (T)Enum.Parse(enumType, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Converts the integer representation of an enum to its enum equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">A integer containing an enum to convert.</param>
        /// <param name="result">When this method returns, contains the enum value equivalent to the integer contained in value, if the conversion succeeded; otherwise contains the default value.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(int value, out T result)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return TryParse(value.ToString(CultureInfo.InvariantCulture), false, out result);
        }

        /// <summary>
        /// Converts the string representation of an enum to its enum equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">A string containing an enum to convert.</param>
        /// <param name="result">When this method returns, contains the enum value equivalent to the string contained in value, if the conversion succeeded; otherwise contains the default value.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(string value, out T result)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return TryParse(value, false, out result);
        }

        /// <summary>
        /// Converts the string representation of an enum to its enum equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">A string containing an enum to convert.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <param name="result">When this method returns, contains the enum value equivalent to the string contained in value, if the conversion succeeded; otherwise contains the default value.</param>
        /// <returns>true if value was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(string value, bool ignoreCase, out T result)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            try
            {
                result = value.ToEnum<T>(ignoreCase);
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        /// Gets a random value from the specified enum type.
        /// </summary>
        /// <typeparam name="T">The Enum to get a random value of.</typeparam>
        /// <returns>A random value from the Enum.</returns>
        public static T RandomEnum<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[RandomUtil.Next(0, values.Length)];
        }

        /// <summary>
        /// Gets a random value from the specified enum type.
        /// </summary>
        /// <typeparam name="T">The Enum to get a random value of.</typeparam>
        /// <param name="min">The minimal index (inclusive).</param>
        /// <param name="max">The maximum index (exclusive).</param>
        /// <returns>A random value between the min and max from the Enum.</returns>
        public static T RandomEnum<T>(int min, int max)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return values[RandomUtil.Next(min, max) % values.Length];
        }

        /// <summary>
        /// Indicates whether the specified enum value has the flag applied.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="value">The Enum that is checked.</param>
        /// <param name="flag">The flag to check.</param>
        /// <returns>true if the flag is applied; otherwise false.</returns>
        public static bool HasFlag<T>(this Enum value, T flag)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            try
            {
                return (Convert.ToInt64(value) & Convert.ToInt64(flag)) == Convert.ToInt64(flag);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates an array with all values of the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <returns>An array with all values.</returns>
        public static T[] ToArray<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        /// <summary>
        /// Creates a typed IEnumerable from Enum values
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <returns>IEnumerable with all values</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            // Can't use type constraints on value types, so have to do check like this
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets all the individual flags for a type. Only works for Enums with the Flag attribute!
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
        {
            return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
        }

        private static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
        {
            ulong bits = Convert.ToUInt64(value);
            var results = new List<Enum>();
            for (int i = values.Length - 1; i >= 0; i--)
            {
                ulong mask = Convert.ToUInt64(values[i]);
                if (i == 0 && mask == 0L)
                    break;

                if ((bits & mask) == mask)
                {
                    results.Add(values[i]);
                    bits -= mask;
                }
            }

            if (bits != 0L)
                return Enumerable.Empty<Enum>();

            if (Convert.ToUInt64(value) != 0L)
                return results.Reverse<Enum>();

            if (bits == Convert.ToUInt64(value) && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
                return values.Take(1);

            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> GetFlagValues(Type enumType)
        {
            ulong flag = 0x1;
            foreach (var value in Enum.GetValues(enumType).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);
                if (bits == 0L)
                {
                    //yield return value;
                    continue; // skip the zero value
                }

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits)
                {
                    yield return value;
                }
            }
        }
    }
}
