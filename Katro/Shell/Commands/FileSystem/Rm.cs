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
    /// Removes a file or directory.
    /// </summary>
    public class Rm : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "rm";

        /// <inheritdoc/>
        public override string Description { get; } = "Removes a file or directory.";

        /// <inheritdoc/>
        public override string Usage { get; } = "rm [--recursive] <path>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
                return CommandReturnCode.MissingArgument;

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            var recursive = ctx.HasFlag("recursive");

            if (File.Exists(path))
                File.Delete(path);
            else if (Directory.Exists(path))
                Directory.Delete(path, recursive);
            else
                return CommandReturnCode.NoEntry;

            return CommandReturnCode.Success;
        }
    }
}
