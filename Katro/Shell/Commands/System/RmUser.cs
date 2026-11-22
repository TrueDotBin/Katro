using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Crypto;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Removes a user.
    /// </summary>
    public sealed class RmUser : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "rmuser";

        /// <inheritdoc/>
        public override string Description { get; } = "Removes a user.";

        /// <inheritdoc/>
        public override string Usage { get; } = "rmuser <username>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;
        
        /// <inheritdoc/>
        public override bool RequiresSuperuser { get; } = true;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var username = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(username))
                return CommandReturnCode.MissingArgument;

            try
            {
                UserManager.RemoveUser(username);
            }
            catch (Exception ex)
            {
                Terminal.WriteLine(ex.Message);
                return CommandReturnCode.GeneralFailure;
            }

            return CommandReturnCode.Success;
        }
    }
}
