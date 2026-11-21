using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Changes the current user.
    /// </summary>
    public sealed class Su : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "su";

        /// <inheritdoc/>
        public override string Description { get; } = "Changes the current user.";

        /// <inheritdoc/>
        public override string Usage { get; } = "su";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            Terminal.AskForUser();
            return CommandReturnCode.Success;
        }
    }
}
