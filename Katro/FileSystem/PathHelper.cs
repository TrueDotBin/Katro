using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Utilities for resolving a path.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Makes <paramref name="path"/> absolute.
        /// </summary>
        public static string Resolve(string cwd, string path)
        {
            var combined = Path.Combine(cwd, path);
            var full = Path.GetFullPath(combined);
            return full;
        }
    }
}
