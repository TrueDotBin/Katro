using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// Base class of a <see cref="Command"/>'s argument.
    /// </summary>
    public class CommandArgument(string name, string description = "", bool required = true)
    {
        /// <summary>
        /// The value of this argument.
        /// </summary>
        public object? Value { get; set; } = null;

        /// <summary>
        /// The name of this argument.
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// The name of this argument.
        /// </summary>
        public string Description { get; set; } = description;

        /// <summary>
        /// Whether this argument is required.
        /// </summary>
        public bool Required { get; init; } = required;

        /// <summary>
        /// Resets this argument's value to the default.
        /// </summary>
        public virtual void ResetValueToDefault()
        {
            Value = null;
        }
    }
}
