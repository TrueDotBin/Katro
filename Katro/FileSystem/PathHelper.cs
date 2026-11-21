using Katro.Configuration;
using Katro.Users;
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
        private static readonly string[] _sensitivePaths =
        [
            UserManager.UserDatabaseFilePath,
            SystemConfig.ConfigFilePath
        ];

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

        /// <summary>
        /// Determines whether the provided path is inside the current user's home directory.
        /// </summary>
        /// <returns>Whether the provided path is inside the current user's home directory.</returns>
        public static bool IsInsideUserPath(string path)
            => path.StartsWith(UserManager.CurrentUser.HomeDirectory);

        /// <summary>
        /// Determines whether the provided path is considered sensitive.
        /// </summary>
        /// <returns>Whether the provided path is considered sensitive.</returns>
        public static bool IsSensitive(string path)
            => _sensitivePaths.Contains(path);
    }
}
