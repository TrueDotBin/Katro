using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// Base class of a <see cref="Command"/>'s argument.
    /// </summary>
    public class CommandArgument
    {
        /// <summary>
        /// The value of this argument.
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// Resets this argument's value to the default.
        /// </summary>
        public void ResetValueToDefault()
        {
            Value = string.Empty;
        }
    }
}
