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
    /// Moves a source file to its destination.
    /// </summary>
    public class Mv : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "mv";

        /// <inheritdoc/>
        public override string Description { get; } = "Moves a source file to its destination.";

        /// <inheritdoc/>
        public override string Usage { get; } = "mv <source> <destination>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var source = ctx.GetArgument(0);
            var destination = ctx.GetArgument(1);

            if (string.IsNullOrEmpty(source) ||
                string.IsNullOrEmpty(destination))
                return CommandReturnCode.MissingArgument;

            source = PathHelper.Resolve(ctx.Shell.CurrentDirectory, source);
            destination = PathHelper.Resolve(ctx.Shell.CurrentDirectory, destination);

            if (!File.Exists(source))
                return CommandReturnCode.NoEntry;

            try
            {
                File.Move(source, destination);
            }
            catch
            {
                // destination is probably on a different mount
                File.Copy(source, destination);
                File.Delete(source);
            }

            return CommandReturnCode.Success;
        }
    }
}
