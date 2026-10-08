using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Base class for all commands.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// The name of this command.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Other names for this command.
        /// </summary>
        public virtual string[] Aliases { get; } = [];

        /// <summary>
        /// What does this command do?
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// How would users use this command?
        /// </summary>
        public abstract string Usage { get; }

        /// <summary>
        /// The available arguments for this command.
        /// </summary>
        public virtual CommandArgument[] Args { get; } = [];

        /// <summary>
        /// Override if you want to dispose of stuff.
        /// </summary>
        public virtual void Cleanup() { }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <returns>The command's return code.</returns>
        public abstract int Run();

        /// <summary>
        /// Attempts to return an option by a key.
        /// </summary>
        /// <param name="key">The key of the target option.</param>
        /// <param name="arg">The option.</param>
        /// <returns>Whether the option was found.</returns>
        public bool TryGetOption(string key, out OptionArgument arg)
        {
            var opt = Args.FirstOrDefault(a => a is OptionArgument opt && (opt.Key == key || opt.ShortKey == key));

            if (opt == null)
            {
                arg = null!;
                return false;
            }

            arg = (OptionArgument)opt;
            return true;
        }

        /// <summary>
        /// Attempts to return a positional argument by its position.
        /// </summary>
        /// <param name="position">The position of the target argument.</param>
        /// <param name="arg">The option.</param>
        /// <returns>Whether the positional argument was found.</returns>
        public bool TryGetPositional(int position, out PositionalArgument arg)
        {
            var pos = Args.FirstOrDefault(a => a is PositionalArgument pos && pos.Position == position);

            if (pos == null)
            {
                arg = null!;
                return false;
            }

            arg = (PositionalArgument)pos;
            return true;
        }
    }
}
