using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Enums
{
    /// <summary>
    /// A <see cref="Command"/>'s return code.
    /// </summary>
    public enum CommandReturnCode
    {
        /// <summary>
        /// The specified command was not found.
        /// </summary>
        CommandNotFound = -1,

        /// <summary>
        /// The command ran with no issues.
        /// </summary>
        Success = 0,

        /// <summary>
        /// General failure.
        /// </summary>
        GeneralFailure = 1
    }
}
