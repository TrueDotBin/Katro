using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Powers off the device, and powers it back on.
    /// </summary>
    public sealed class Reboot : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "reboot";

        /// <inheritdoc/>
        public override string Description { get; } = "Powers off the device, and powers it back on.";

        /// <inheritdoc/>
        public override string Usage { get; } = "reboot";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            PowerManager.Reboot();
            return CommandReturnCode.Success;
        }
    }
}
