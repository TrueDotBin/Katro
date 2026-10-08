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

        /// <summary>
        /// Runs a command by input.
        /// </summary>
        /// <param name="input">The input to run.</param>
        /// <returns>The command's return code.</returns>
        public static int Run(string input)
        {
            var firstSpace = input.IndexOf(' ');

            if (firstSpace == -1)
            {
                return Run(input, ""); // there are no args
            }
            else
            {
                var name = input.Substring(0, firstSpace);
                var args = input.Substring(firstSpace);

                return Run(name, args);
            }
        }

        /// <summary>
        /// Runs a command by name.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="args">The arguments to provide to the command.</param>
        /// <returns>The command's return code.</returns>
        public static int Run(string name, string args)
        {
            var command = s_commands.FirstOrDefault(c => c.Name == name || c.Aliases.Contains(name));

            if (command == null)
                return (int)CommandReturnCode.CommandNotFound;

            ArgumentParser.Parse(args, command);

            var result = command.Run();

            foreach (var arg in command.Args)
                arg.ResetValueToDefault();

            return result;
        }
    }
}