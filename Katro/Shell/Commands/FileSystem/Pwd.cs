using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Shell.Commands.FileSystem
{
    /// <summary>
    /// Displays the current working directory.
    /// </summary>
    public class Pwd : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "pwd";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays the current working directory.";

        /// <inheritdoc/>
        public override string Usage { get; } = "pwd";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            Terminal.WriteLine(ctx.Shell.CurrentDirectory);
            return CommandReturnCode.Success;
        }
    }
}
