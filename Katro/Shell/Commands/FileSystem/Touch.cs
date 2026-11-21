using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Shell.Commands.FileSystem
{
    /// <summary>
    /// Creates a new blank file.
    /// </summary>
    public class Touch : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "touch";

        /// <inheritdoc/>
        public override string Description { get; } = "Creates a new blank file.";

        /// <inheritdoc/>
        public override string Usage { get; } = "touch <path>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
                return CommandReturnCode.MissingArgument;

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            File.Create(path).Close();

            return CommandReturnCode.Success;
        }
    }
}
