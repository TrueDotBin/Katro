using Katro.Constants.Shell;
using Katro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.Audio
{
    /// <summary>
    /// Makes a beep for a specified amount of time.
    /// </summary>
    public sealed class Beep : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "beep";

        /// <inheritdoc/>
        public override string Description { get; } = "Makes a beep for a specified amount of time.";

        /// <inheritdoc/>
        public override string Usage { get; } = "beep [frequency] [duration]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Audio;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var freqRaw = ctx.GetArgument(0);
            var durRaw = ctx.GetArgument(1);

            if (!int.TryParse(freqRaw, out var frequency))
                frequency = 800;

            if (!int.TryParse(durRaw, out var duration))
                duration = 200;

            Console.Beep(frequency, duration);

            return CommandReturnCode.Success;
        }
    }
}
