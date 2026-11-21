using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.Core
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
        public override CommandCategory Category { get; } = CommandCategory.Core;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var argument = ctx.GetArgument(0);
            Terminal.WriteLine(argument);

            return CommandReturnCode.Success;
        }
    }
}
