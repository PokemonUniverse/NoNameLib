using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NoNameLib.Debug;

namespace NoNameLib.Extension
{
    public static class StringUtil
    {
        #region Comparison methods

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs within this string.
        /// </summary>
        /// <param name="original">The original System.String object.</param>
        /// <param name="value">The System.String object to seek.</param>
        /// <param name="comparisionType">One of the System.StringComparison values.</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this string original, string value, StringComparison comparisionType)
        {
            return original.IndexOf(value, comparisionType) >= 0;
        }

        /// <summary>
        /// Validate if a string is numeric (containing only numbers)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            char[] ca = s.ToCharArray();
            return ca.All(char.IsNumber);
        }

        /// <summary>
        /// Indicates whether the specified System.String object is null, a System.String.Empty string or a System.String that only contains white space.
        /// </summary>
        /// <param name="value">A System.String reference.</param>
        /// <returns>true if the value parameter is null, an empty string ("") or contains only white space; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrEmpty(value) || value.All(Char.IsWhiteSpace);
        }

        /// <summary>
        /// Indicates whether the specified System.String instance only contains white space.
        /// </summary>
        /// <param name="value">A System.String reference</param>
        /// <returns>True if the value only contains white space, false if not.</returns>
        public static bool IsOnlyWhiteSpace(string value)
        {
            return value.All(Char.IsWhiteSpace);
        }

        /// <summary>
        /// Indicates whether the specified System.String object is null, a System.String.Empty string or a System.String that only contains white space.
        /// </summary>
        /// <param name="value">A System.String reference.</param>
        /// <returns>true if the value parameter is null, an empty string ("") or contains only whit espace; otherwise, false.</returns>
        [Obsolete("Use IsNullOrWhiteSpace instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsNullOrBlank(string value)
        {
            return IsNullOrWhiteSpace(value);
        }

        #endregion

        #region Combiner methods

        /// <summary>
        /// Use this to combine infinite parts with a specified seperator
        /// </summary>
        /// <param name="seperator">Seperator</param>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithSeperator(string seperator, params object[] args)
        {
            var toReturn = new StringBuilder();

            for (int i = 0; args != null && i < args.Length; i++)
            {
                string value = String.Empty;

                if (args[i] != null)
                {
                    value = args[i].ToString();
                }

                if (value.Length > 0)
                {
                    if (toReturn.Length > 0)
                    {
                        toReturn.Append(seperator);
                        toReturn.Append(value);
                    }
                    else
                    {
                        toReturn.Append(value);
                    }
                }
            }

            return toReturn.ToString();
        }

        /// <summary>
        /// Use this to combine parts with a forward slash (eg. for URLs). When done all double sepeartors are removed.
        /// </summary>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithForwardSlash(params object[] args)
        {
            string output = CombineWithSeperator("/", args);

            // RB: Removed, since args could have "//" in it (and we don't want to remove it from there!)
            //output = Regex.Replace(output, "//", "/");

            return output;
        }

        /// <summary>
        /// Use this to combine infinite parts with a comma
        /// </summary>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithComma(params object[] args)
        {
            return CombineWithSeperator(",", args);
        }

        /// <summary>
        /// Use this to combine infinite parts with a comma and space ', '
        /// </summary>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithCommaSpace(params object[] args)
        {
            return CombineWithSeperator(", ", args);
        }

        /// <summary>
        /// Use this to combine infinite parts with a space
        /// </summary>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithSpace(params object[] args)
        {
            return CombineWithSeperator(" ", args);
        }

        /// <summary>
        /// Use this to combine 2 parts with a semicolon+space seperator
        /// string.Empty + "dummy" returns "dummy" (no seperator)
        /// "test" + "dummy" returns "test;[space]dummy"
        /// </summary>
        /// <param name="args">Elements to combine</param>
        /// <returns>Combined string with seperator if both a and b have a value</returns>
        public static string CombineWithSemicolon(params object[] args)
        {
            return CombineWithSeperator(";", args);
        }

        /// <summary>
        /// Use this to combine infinite parts with a specified seperator (and additional seperator for the last item).
        /// </summary>
        /// <param name="seperator">The seperator text to add between the items.</param>
        /// <param name="seperatorLast">The seperator text to add before the last item.</param>
        /// <param name="args">The array of elements to combine.</param>
        /// <returns>Combined string with the specified seperators.</returns>
        public static string CombineWithAdditionalSeperator(string seperator, string seperatorLast, params object[] args)
        {
            var toReturn = new StringBuilder();

            for (int i = 0; i < args.Length; i++)
            {
                string value = String.Empty;
                if (args[i] != null)
                {
                    value = args[i].ToString();
                }

                if (value.Length > 0)
                {
                    if (toReturn.Length > 0 && i == args.Length - 1)
                    {
                        toReturn.Append(seperatorLast);
                        toReturn.Append(value);
                    }
                    else if (toReturn.Length > 0)
                    {
                        toReturn.Append(seperator);
                        toReturn.Append(value);
                    }
                    else
                    {
                        toReturn.Append(value);
                    }
                }
            }

            return toReturn.ToString();
        }

        #endregion

        #region String.Formating

        /// <summary>
        /// Replaces the format item in a specified <see cref="System.String"/> with the text equivalent of the value of a corresponding <see cref="System.Object"/> instance in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the <see cref="System.String"/> equivalent of the corresponding instances of <see cref="System.Object"/> in <paramref name="args"/>.</returns>
        public static string FormatWith(string format, params object[] args)
        {
            return FormatSafe(format, args);
        }

        /// <summary>
        /// Replaces the format item in a specified <see cref="System.String"/> with the text equivalent of the value of a corresponding <see cref="System.Object"/> instance in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more objects to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the <see cref="System.String"/> equivalent of the corresponding instances of <see cref="System.Object"/> in <paramref name="args"/>.</returns>
        public static string FormatWith(string format, IFormatProvider provider, params object[] args)
        {
            return FormatSafeWithProvider(format, provider, args);
        }

        /// <summary>
        /// Replaces the format item in a specified <see cref="System.String"/> with the text equivalent of the value of a data-binding expression using a specified <see cref="System.Object"/> instance.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An <see cref="System.Object"/> to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the <see cref="System.String"/> equivalant of the corresponding instances of the data-binding expression using <paramref name="arg0"/>.</returns>
        public static string FormatWith(string format, object arg0)
        {
            return FormatWith(format, null, arg0);
        }

        /// <summary>
        /// Format a string using string.Format but with a fall back, if the arguments don't fit the format it won't throw an Exception
        /// </summary>
        /// <param name="format">Format</param>
        /// <param name="args">Arguments</param>
        /// <returns>
        /// The string.Format result, if it fails it concats the format with the arguments and prefixes it with: Invalid String Format:
        /// </returns>
        public static string FormatSafe(this String format, params object[] args)
        {
            string formattedText;

            try
            {
                formattedText = string.Format(format, args);
            }
            catch
            {
                if (TestUtil.IsPcDeveloper)
                    formattedText = string.Format("Invalid String Format: '{0}', Args: {1}", format, CombineWithSeperator(", ", args));
                else
                    formattedText = string.Format("{0} ({1})", format, CombineWithSeperator(", ", args));
            }

            return formattedText;
        }

        /// <summary>
        /// Format a string using string.Format but with a fall back, if the arguments don't fit the format it won't throw an Exception
        /// </summary>
        /// <param name="format">Format</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="args">Arguments</param>
        /// <returns>
        /// The string.Format result, if it fails it concats the format with the arguments and prefixes it with: Invalid String Format:
        /// </returns>
        public static string FormatSafeWithProvider(this String format, IFormatProvider formatProvider, params object[] args)
        {
            string formattedText;

            try
            {
                if (formatProvider == null)
                    formattedText = string.Format(format, args);
                else
                    formattedText = string.Format(formatProvider, format, args);
            }
            catch
            {
                formattedText = string.Format("Invalid String Format (with formatProvider): '{0}', Args: {1}", format, CombineWithSeperator(", ", args));
            }

            return formattedText;
        }

        #endregion

        #region Casing

        /// <summary>
        /// Converts a String to PascalCase (eg. "pascal case" and "pascal-case" become "PascalCase").
        /// </summary>
        /// <param name="input">The String to convert.</param>
        /// <returns>The converted String.</returns>
        public static string ToPascalCase(this string input)
        {
            return Regex.Replace(input, @"(^|\W)(?<Char>\w)", m => m.Groups["Char"].Value.ToUpperInvariant(), RegexOptions.ExplicitCapture | RegexOptions.Singleline);
        }

        #endregion

        public static string ToOneCharString(this char c)
        {
            return new string(c, 1);
        }
    }
}
