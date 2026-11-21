using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Who are you?
    /// </summary>
    public sealed class Whoami : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "whoami";

        /// <inheritdoc/>
        public override string Description { get; } = "Who are you?";

        /// <inheritdoc/>
        public override string Usage { get; } = "whoami";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var username = UserManager.CurrentUser.Username;
            Terminal.WriteLine(username);

            return CommandReturnCode.Success;
        }
    }
}
