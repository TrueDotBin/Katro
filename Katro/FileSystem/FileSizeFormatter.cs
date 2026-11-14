using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Formats bytes into a readable string.
    /// </summary>
    public static class FileSizeFormatter
    {
        /// <summary>
        /// Formats the <paramref name="size"/> to a readable string, e.g., "10 GB"
        /// </summary>
        /// <param name="size">The size in bytes.</param>
        public static string FormatSize(ulong size)
        {
            string[] suffixes = ["B", "KB", "MB", "GB", "TB"];

            var counter = 0;
            var number = (decimal)size;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }
    }
}
