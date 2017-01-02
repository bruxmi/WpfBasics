using System;

namespace WpfViewModelBasics.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Compares to strings for equality, ignoring casing.
        /// </summary>
        /// <param name="str">
        ///     The 1st string. Extension methods don't use virtual calls,
        ///     so if str is null, no exception is thrown.
        /// </param>
        /// <param name="str2">
        ///     The 2nd string.
        /// </param>
        /// <returns>
        ///     Returns <c>true</c> if both strings are equal.
        ///     Otherwise <c>false</c> is returned.
        /// </returns>
        public static bool EqualsIgnoreCase(this string str, string str2)
        {
            return string.Compare(str, str2, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
