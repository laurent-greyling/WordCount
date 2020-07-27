using System;
using System.Globalization;
using System.Linq;

namespace WordCount.Library.Utilities
{
    /// <summary>
    /// Class that contains the basic method parameter checks
    /// </summary>
    public class Ensure
    {
        /// <summary>
        /// Having an attibute named 'ValidatedNotNull' allows us to defer parameter checking
        /// to a separate class and prevents firing CA1062 unnecessarily.
        /// </summary>
        [AttributeUsage(AttributeTargets.Parameter)]
        private sealed class ValidatedNotNullAttribute : Attribute { }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="name">The name of the parameter, which will appear in the exception message.</param>
        public static void ArgumentNotNull([ValidatedNotNull] object value, string name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the specified value is <see langword="null"/>
        /// or an empty string.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="name">The name of the parameter, which will appear in the exception message.</param>
        public static void ArgumentNotNullOrEmptyString([ValidatedNotNull] string value, string name)
        {
            ArgumentNotNull(value, name);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture,
                        "The argument '{0}' cannot be an empty string",
                        name), name);
            }
        }

        /// <summary>
        /// check if all arguments are null, if so return true
        /// If true, need to handle all null scenario
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsCommandArgumentsNotNull(params object[] values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (value == null)
                {
                    count++;
                    continue;
                }

                var type = value.GetType();

                if (type.IsValueType)
                {
                    int.TryParse(value.ToString(), out int intValue);
                    if (intValue == 0)
                    {
                        count++;
                    }
                }
            }

            return count == values.Count();
        }

        /// <summary>
        /// check if all arguments are null, if so return true
        /// If true, need to handle all null scenario
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsCommandArgumentsNotNullOrEmptyString(params string[] values)
        {
            var count = 0;
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    count++;
                    continue;
                }                
            }

            return count == values.Count();
        }
    }
}
