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
    /// Creates a new blank directory.
    /// </summary>
    public class Mkdir : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "mkdir";

        /// <inheritdoc/>
        public override string Description { get; } = "Creates a new blank directory.";

        /// <inheritdoc/>
        public override string Usage { get; } = "mkdir <path>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);
            
            if (string.IsNullOrEmpty(path))
                return CommandReturnCode.MissingArgument;

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            Directory.CreateDirectory(path);
            return CommandReturnCode.Success;
        }
    }
}
