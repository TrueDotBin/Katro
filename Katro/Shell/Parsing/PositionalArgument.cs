using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// A <see cref="Command"/>'s positional argument.
    /// </summary>
    public class PositionalArgument : CommandArgument
    {
        /// <summary>
        /// The position of this argument.
        /// </summary>
        public int Position { get; init; }
    }
}
