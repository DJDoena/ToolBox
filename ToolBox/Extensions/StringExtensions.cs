using System;
using System.IO;
using System.Linq;

namespace DoenaSoft.ToolBox.Extensions
{
    /// <summary />
    public static class StringExtensions
    {
        /// <summary>
        /// Replacement for <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false</returns>
        public static bool IsEmpty(this string value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Inversion of <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="value">the string to test</param>
        /// <returns>true if the value parameter is not null and not an empty string (""); otherwise, false</returns>
        public static bool IsNotEmpty(this string value)
            => value.IsEmpty() == false;

        /// <summary>
        /// Replaces all characters in a string that are not allowed in a file name with a replacement character.
        /// </summary>
        /// <param name="fileName">the file name</param>
        /// <param name="replacement">the replacement character</param>
        /// <returns>a valid file name</returns>
        public static string ReplaceInvalidFileNameChars(this string fileName
            , Char replacement)
        {
            if (fileName.IsEmpty())
            {
                return (fileName);
            }

            Char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            if (invalidFileNameChars.Contains(replacement))
            {
                throw (new ArgumentException("Replacement character is also an invalid file name character", nameof(replacement)));
            }

            string newFileName = new string(fileName.Select(original => invalidFileNameChars.Contains(original) ? replacement : original).ToArray());

            return (newFileName);
        }

        /// <summary>
        /// Replaces all characters in a string that are not allowed in a path with a replacement character.
        /// </summary>
        /// <param name="path">the path</param>
        /// <param name="replacement">the replacement character</param>
        /// <returns>a valid path</returns>
        public static string ReplaceInvalidPathChars(this string path
            , Char replacement)
        {
            if (path.IsEmpty())
            {
                return (path);
            }

            Char[] invalidPathChars = Path.GetInvalidPathChars();

            if (invalidPathChars.Contains(replacement))
            {
                throw (new ArgumentException("Replacement character is also an invalid path character", nameof(replacement)));
            }

            string newPath = new string(path.Select(original => invalidPathChars.Contains(original) ? replacement : original).ToArray());

            return (newPath);
        }
    }
}