using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// A <see cref="Command">'s option.
    /// </summary>
    public class OptionArgument : CommandArgument
    {
        /// <summary>
        /// The key of this argument, e.g., "--help"
        /// </summary>
        public string? Key { get; init; }

        /// <summary>
        /// The short key of this argument, e.g., "-h"
        /// </summary>
        public string? ShortKey { get; init; }
    }
}
