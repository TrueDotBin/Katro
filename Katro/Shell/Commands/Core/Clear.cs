using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.Core
{
    /// <summary>
    /// Clears the screen.
    /// </summary>
    public sealed class Clear : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "clear";

        /// <inheritdoc/>
        public override string Description { get; } = "Clears the screen.";

        /// <inheritdoc/>
        public override string Usage { get; } = "clear";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Core;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            Terminal.Clear();
            return CommandReturnCode.Success;
        }
    }
}
