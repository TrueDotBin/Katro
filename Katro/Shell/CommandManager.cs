using Katro.Constants;
using Katro.Shell.Commands;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell
{
    /// <summary>
    /// Manages all <see cref="Command"/>s.
    /// </summary>
    public static class CommandManager
    {
        private static List<Command> s_commands = [];

        /// <summary>
        /// The list of all registered commands.
        /// </summary>
        public static IReadOnlyList<Command> Commands => s_commands;

        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="command">The command to register.</param>
        public static void Register(Command command)
        {
            s_commands.Add(command);
        }
    }
}