namespace DoenaSoft.ToolBox.Extensions
{
    using System;

    /// <summary />
    public static class StringExtensions
    {
        /// <summary>
        /// Replacement for <see cref="String.IsNullOrEmpty(String)"/>.
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false</returns>
        public static Boolean IsEmpty(this String value)
            => (String.IsNullOrEmpty(value));

        /// <summary>
        /// Inversion of <see cref="String.IsNullOrEmpty(String)"/>.
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the value parameter is not null and not an empty string (""); otherwise, false</returns>
        public static Boolean IsNotEmpty(this String value)
            => (value.IsEmpty() == false);
    }
}