using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Utils
{
    /// <summary>
    /// Utility class for getting the Limine "cmdline" entry args.
    /// </summary>
    public static class CmdLineUtils
    {
        /// <summary>
        /// Determines whether the target argument was found in the provided command-line arguments.
        /// </summary>
        /// <param name="target">The target argument.</param>
        /// <returns>Whether the target argument was found in the provided command-line arguments.</returns>
        public static bool HasArg(string target)
        {
            var args = Environment.GetCommandLineArgs();
            return args.FirstOrDefault(a => a == target) != null;
        }
    }
}
