using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Lists all available commands, or information about a command.
    /// </summary>
    public sealed class Help : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "help";

        /// <inheritdoc/>
        public override string Description { get; } = "Lists all available commands, or information about a command.";

        /// <inheritdoc/>
        public override string Usage { get; } = "help [command]";

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var commandName = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(commandName))
            {
                var startColor = Console.ForegroundColor;

                foreach (var cmd in ctx.Shell.Commands)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(cmd.Name);
                    Console.ForegroundColor = startColor;

                    Console.Write(" - ");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(cmd.Description);
                    Console.ForegroundColor = startColor;
                }

                return 0;
            }

            var command = ctx.Shell.Commands.FirstOrDefault(c => c.Name == commandName);

            if (command is null)
                return -1;

            Console.WriteLine($"{command.Name} - {command.Description}");
            Console.WriteLine($"Usage: {command.Usage}");

            return 0;
        }
    }
}
