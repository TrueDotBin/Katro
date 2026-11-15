using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Constants.Shell
{
    /// <summary>
    /// Return codes for commands, e.g., 0 for success, 1 for general failure, etc.
    /// </summary>
    public static class CommandReturnCode
    {
        /// <summary>
        /// Indicates that a command has not been found.
        /// </summary>
        public const int CommandNotFound = -1;

        /// <summary>
        /// Indicates that this command ran successfully.
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// Indicates a general failure.
        /// </summary>
        public const int GeneralFailure = 1;

        /// <summary>
        /// Indicates that a file or directory was not found.
        /// </summary>
        public const int NoEntry = 2;
    }
}
