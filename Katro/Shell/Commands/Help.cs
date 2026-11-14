using Katro.Constants.Shell;
using Katro.Enums;
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
        public override CommandCategory Category { get; } = CommandCategory.Core;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var commandName = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(commandName))
            {
                var startColor = Console.ForegroundColor;

                var groups = ctx.Shell.Commands
                    .GroupBy(c => c.Category)
                    .OrderBy(g => g.Key);

                foreach (var group in groups)
                {
                    Console.WriteLine($"===== {group.Key} =====");

                    foreach (var cmd in group.OrderBy(c => c.Name))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{cmd.Name,-12}");

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(cmd.Description);
                        Console.ForegroundColor = startColor;
                    }
                }

                return CommandReturnCode.Success;
            }

            var command = ctx.Shell.Commands.FirstOrDefault(c => c.Name == commandName);

            if (command is null)
                return CommandReturnCode.CommandNotFound;

            Console.WriteLine($"{command.Name} - {command.Description}");
            Console.WriteLine($"Usage: {command.Usage}");
            Console.WriteLine($"Category: {command.Category}");

            return CommandReturnCode.Success;
        }
    }
}
