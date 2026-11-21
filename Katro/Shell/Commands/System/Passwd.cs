using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Crypto;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Changes the password of a user.
    /// </summary>
    public sealed class Passwd : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "passwd";

        /// <inheritdoc/>
        public override string Description { get; } = "Changes the password of a user.";

        /// <inheritdoc/>
        public override string Usage { get; } = "passwd <username>";

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

            var user = UserManager.GetUsers().FirstOrDefault(u => u.Username == username);

            if (user is null)
                return CommandReturnCode.GeneralFailure;

            Terminal.Write("Enter new password: ");
            var input = Console.ReadLine();

            if (input is null)
                return CommandReturnCode.GeneralFailure;

            var hashed = PasswordHasher.Hash(user.Salt, input);
            user.Hash = hashed;

            Terminal.WriteLine($"Successfully changed the password for {user.Username}", ConsoleColor.Green);

            return CommandReturnCode.Success;
        }
    }
}
