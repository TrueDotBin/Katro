using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Displays a user-specified message.
    /// </summary>
    public sealed class Echo : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "echo";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays a user-specified message.";

        /// <inheritdoc/>
        public override string Usage { get; } = "echo [message]";

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var argument = ctx.GetArgument(0);
            Console.WriteLine(argument);

            return 0;
        }
    }
}
