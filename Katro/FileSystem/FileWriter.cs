using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Utility class for writing to files only if they don't exist.
    /// This prevents overwriting the user's modifications when, for example, files are loaded from embedded resources and written on every boot.
    /// </summary>
    public static class FileWriter
    {
        /// <summary>
        /// Writes raw bytes to a file.
        /// </summary>
        /// <param name="path">The path of the file to write to.</param>
        /// <param name="bytes">The raw bytes to write.</param>
        public static void WriteBytes(string path, params byte[] bytes)
        {
            if (!File.Exists(path))
                File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="path">The path of the file to write to.</param>
        /// <param name="text">The text to write.</param>
        public static void WriteText(string path, string text)
        {
            if (!File.Exists(path))
                File.WriteAllText(path, text);
        }
    }
}
