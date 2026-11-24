using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Sends a ping request to a server.
    /// </summary>
    public sealed class Ping : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "ping";

        /// <inheritdoc/>
        public override string Description { get; } = "Sends a ping request to a server.";

        /// <inheritdoc/>
        public override string Usage { get; } = "ping <domain> [--secondary] [--times=1] [--timeout=5000]";

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
            var times = 1;

            if (ctx.TryGetOption("timeout", out var timeoutStr) && !int.TryParse(timeoutStr, out timeout))
                return CommandReturnCode.GeneralFailure;

            if (ctx.TryGetOption("times", out var timesStr) && !int.TryParse(timesStr, out times))
                return CommandReturnCode.GeneralFailure;

            var addr = DnsResolver.Resolve(domain, secondary, timeout);

            if (addr is not null)
                Terminal.WriteLine($"Pinging {domain} [{addr}]");
            else
                return CommandReturnCode.GeneralFailure;

            Terminal.WriteLine();

            var failed = 0;
            var succeeded = 0;

            for (int i = 0; i < times; i++)
            {
                var result = Pinger.Ping(addr, timeout);

                if (result.Time == -1)
                {
                    Terminal.WriteLine("Ping failed", ConsoleColor.Red);
                    failed++;
                }
                else
                {
                    Terminal.WriteLine($"Reply from {domain}: time={result.Time}ms");
                    succeeded++;
                }

                if (i < times - 1)
                    Thread.Sleep(1000);
            }

            Terminal.WriteLine();

            Terminal.Heading("Ping Statistics");
            Terminal.InfoLine("Sent", times);
            Terminal.InfoLine("Succeeded", succeeded);
            Terminal.InfoLine("Failed", failed);

            return CommandReturnCode.Success;
        }
    }
}
