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
        /// <param name="cwd">The current working directory.</param>
        /// <param name="path">The path that should be made absolute.</param>
        public static string Resolve(string cwd, string path)
        {
            if (Path.IsPathRooted(path))
                return path;

            var combined = Path.Combine(cwd, path);
            var full = Path.GetFullPath(combined);
            return full;
        }
    }
}
