using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Powers off the device.
    /// </summary>
    public sealed class Shutdown : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "shutdown";

        /// <inheritdoc/>
        public override string Description { get; } = "Powers off the device.";

        /// <inheritdoc/>
        public override string Usage { get; } = "shutdown";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            PowerManager.Shutdown();
            return CommandReturnCode.Success;
        }
    }
}
