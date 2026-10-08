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
        public abstract string[] Aliases { get; }

        /// <summary>
        /// What does this command do?
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Override if you want to dispose of stuff.
        /// </summary>
        public virtual void Cleanup() { }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <returns>The command's return code.</returns>
        public abstract int Run(); // TODO: implement argument parsing
    }
}
