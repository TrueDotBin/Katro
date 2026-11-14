using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Lists all available commands.
    /// </summary>
    public sealed class Help : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "help";

        /// <inheritdoc/>
        public override string Description { get; } = "Lists all available commands.";

        /// <inheritdoc/>
        public override string Usage { get; } = "help";

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var startColor = Console.ForegroundColor;

            foreach (var cmd in ctx.Shell.Commands)
            {
                Console.Write($"{cmd.Name} - ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(cmd.Description);
                Console.ForegroundColor = startColor;
            }

            return 0;
        }
    }
}
