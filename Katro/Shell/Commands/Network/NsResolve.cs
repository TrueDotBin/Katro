using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Resolves a nameserver.
    /// </summary>
    public sealed class NsResolve : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "nsresolve";

        /// <inheritdoc/>
        public override string Description { get; } = "Resolves a nameserver.";

        /// <inheritdoc/>
        public override string Usage { get; } = "nsresolve <domain> [--secondary] [--timeout=5000]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Network;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var domain = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(domain))
                return CommandReturnCode.MissingArgument;

            var secondary = ctx.HasFlag("secondary");
            var timeout = 5000;

            if (ctx.TryGetOption("timeout", out var timeoutStr) && !int.TryParse(timeoutStr, out timeout))
                return CommandReturnCode.GeneralFailure;

            var addr = DnsResolver.Resolve(domain, secondary, timeout);

            if (addr is not null)
                Terminal.WriteLine($"{domain} -> {addr}", ConsoleColor.Green);
            else
                return CommandReturnCode.GeneralFailure;

            return CommandReturnCode.Success;
        }
    }
}
