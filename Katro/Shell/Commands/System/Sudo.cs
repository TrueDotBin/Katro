using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Crypto;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Elevates the user's permissions, and runs a command.
    /// </summary>
    public sealed class Sudo : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "sudo";

        /// <inheritdoc/>
        public override string Description { get; } = "Elevates the user's permissions, and runs a command.";

        /// <inheritdoc/>
        public override string Usage { get; } = "sudo <command> [arguments]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var firstSpace = ctx.Input.IndexOf(' ');
            if (firstSpace == -1 || firstSpace == ctx.Input.Length - 1)
                return CommandReturnCode.MissingArgument;

            UserManager.ShowLecture();

            var currentUser = UserManager.CurrentUser;

            if (!UserManager.IsType(UserType.Superuser))
            {
                Terminal.Write($"Password for {currentUser.Username}: ");
                var input = Console.ReadLine();

                if (input is null)
                    return CommandReturnCode.GeneralFailure;

                if (!PasswordHasher.Verify(input, currentUser.Hash, currentUser.Salt))
                {
                    Terminal.WriteLine("Sorry, try again.");
                    return CommandReturnCode.GeneralFailure;
                }
            }

            var cmdLine = ctx.Input.Substring(firstSpace + 1).Trim();

            UserManager.Elevated = true;

            try
            {
                ctx.Shell.ProcessInput(cmdLine);
            }
            finally
            {
                UserManager.Elevated = false;
            }

            return CommandReturnCode.Success;
        }
    }
}
