using Katro.Constants.Shell;
using Katro.Enums;
using Cosmos.Kernel.System.Diagnostics;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Triggers the garbage collector.
    /// </summary>
    public sealed class Gc : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "gc";

        /// <inheritdoc/>
        public override string Description { get; } = "Triggers the garbage collector.";

        /// <inheritdoc/>
        public override string Usage { get; } = "gc";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var freed = MemoryDiagnostics.Collect();
            Console.WriteLine($"{freed} object(s) freed");

            return CommandReturnCode.Success;
        }
    }
}
