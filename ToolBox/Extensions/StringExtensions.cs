namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.IO;
    using System.Linq;

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

        /// <summary>
        /// Replaces all characters in a string that are not allowed in a file name with a replacement character.
        /// </summary>
        /// <param name="fileName">the file name</param>
        /// <param name="replacement">the replacement character</param>
        /// <returns>a valid file name</returns>
        public static String ReplaceInvalidFileNameChars(this String fileName
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

            String newFileName = fileName;

            foreach (Char invalid in invalidFileNameChars)
            {
                newFileName = newFileName.Replace(invalid, replacement);
            }

            return (newFileName);
        }

        /// <summary>
        /// Replaces all characters in a string that are not allowed in a path with a replacement character.
        /// </summary>
        /// <param name="path">the path</param>
        /// <param name="replacement">the replacement character</param>
        /// <returns>a valid path</returns>
        public static String ReplaceInvalidPathChars(this String path
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

            String newPath = path;

            foreach (Char invalid in invalidPathChars)
            {
                newPath = newPath.Replace(invalid, replacement);
            }

            return (newPath);
        }
    }
}