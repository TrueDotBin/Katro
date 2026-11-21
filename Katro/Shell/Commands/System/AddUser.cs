using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Crypto;
using Katro.Enums;
using Katro.Output;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    public sealed class AddUser : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "adduser";

        /// <inheritdoc/>
        public override string Description { get; } = "Creates a new user.";

        /// <inheritdoc/>
        public override string Usage { get; } = "adduser <username> [password] [type]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;
        
        /// <inheritdoc/>
        public override bool RequiresSuperuser { get; } = true;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var username = ctx.GetArgument(0);
            var password = ctx.GetArgument(1);
            var typeStr = ctx.GetArgument(2);

            if (string.IsNullOrEmpty(username))
                return CommandReturnCode.MissingArgument;

            if (string.IsNullOrEmpty(password))
            {
                Terminal.WriteLine("This account won't have a password.");
                Terminal.WriteLine("It is suggested that you provide a password for user accounts.");
                password = string.Empty;
            }
            
            if (string.IsNullOrEmpty(typeStr))
                typeStr = UserType.Normal.ToString();

            if (!Enum.TryParse<UserType>(typeStr, true, out var type))
                return CommandReturnCode.MissingArgument;

            UserManager.AddUser(username, password, type);
            return CommandReturnCode.Success;
        }
    }
}
