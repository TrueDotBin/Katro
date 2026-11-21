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
    /// Changes the current working directory.
    /// </summary>
    public class Cd : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "cd";

        /// <inheritdoc/>
        public override string Description { get; } = "Changes the current working directory.";

        /// <inheritdoc/>
        public override string Usage { get; } = "cd <path>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);
            
            if (string.IsNullOrEmpty(path))
                return CommandReturnCode.MissingArgument;

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            if (!Directory.Exists(path))
                return CommandReturnCode.NoEntry;

            ctx.Shell.CurrentDirectory = path;

            return CommandReturnCode.Success;
        }
    }
}
