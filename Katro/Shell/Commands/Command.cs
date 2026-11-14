using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// A command that can be executed with arguments.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// The name of this command.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// What this command does.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// How this command should be used.
        /// </summary>
        public abstract string Usage { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments to pass to the command.</param>
        /// <returns>The command's return code.</returns>
        public abstract int Execute(string[] args);
    }
}
