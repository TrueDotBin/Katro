using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Enums
{
    /// <summary>
    /// The category of a command, e.g., file system
    /// </summary>
    public enum CommandCategory
    {
        /// <summary>
        /// This command doesn't interact with other subsystems.
        /// </summary>
        Core,

        /// <summary>
        /// This command uses the file system.
        /// </summary>
        FileSystem,

        /// <summary>
        /// This command inspects or modifies the system's state.
        /// </summary>
        System,

        /// <summary>
        /// This command makes sound.
        /// </summary>
        Audio
    }
}
