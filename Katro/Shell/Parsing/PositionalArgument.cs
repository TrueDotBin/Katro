using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// A <see cref="Command"/>'s positional argument.
    /// </summary>
    public class PositionalArgument(string name, int position, string description = "", bool required = true)
        : CommandArgument(name, description, required)
    {
        /// <summary>
        /// The position of this argument.
        /// </summary>
        public int Position { get; init; } = position;
    }
}
