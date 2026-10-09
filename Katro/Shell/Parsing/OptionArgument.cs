using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// A <see cref="Command">'s option.
    /// </summary>
    public class OptionArgument(string key, string shortKey = "", string description = "", bool required = true)
        : CommandArgument(key, description, required)
    {
        /// <summary>
        /// The key of this argument, e.g., "--help"
        /// </summary>
        public string Key { get; init; } = key;

        /// <summary>
        /// The short key of this argument, e.g., "-h"
        /// </summary>
        public string ShortKey { get; init; } = shortKey;

        public override void ResetValueToDefault()
        {
            Value = false;
        }
    }
}
