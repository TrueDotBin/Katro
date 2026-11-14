using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell
{
    /// <summary>
    /// A command's context, containing the user's input, flags, options and arguments.
    /// </summary>
    public class CommandContext(string input)
    {
        /// <summary>
        /// The user's input.
        /// </summary>
        public string Input { get; init; } = input;

        /// <summary>
        /// A list of arguments.
        /// </summary>
        public List<string> Args { get; init; } = [];

        /// <summary>
        /// A list of flags (if they're present, their value is true)
        /// </summary>
        public List<string> Flags { get; init; } = [];

        /// <summary>
        /// A list of options.
        /// </summary>
        public Dictionary<string, string> Options { get; init; } = [];

        /// <summary>
        /// Checks whether the user has given the provided <paramref name="flag"/>.
        /// </summary>
        /// <param name="flag">The flag's name, e.g., "force"</param>
        /// <returns>Whether that flag is present.</returns>
        public bool HasFlag(string flag)
            => Flags.Any(f => f == flag);

        /// <summary>
        /// Tries to get an option's value.
        /// </summary>
        /// <param name="opt">The name of the option to look for.</param>
        /// <param name="value">The output value of this option.</param>
        /// <returns>Whether the option was found.</returns>
        public bool TryGetOption(string opt, out string value)
            => Options.TryGetValue(opt, out value!);

        /// <summary>
        /// Gets an argument's value.
        /// </summary>
        /// <param name="position">The index of the argument.</param>
        public string GetArgument(int position)
        {
            if (position < 0 || position > Args.Count)
                return string.Empty;

            return Args[position];
        }
    }
}
