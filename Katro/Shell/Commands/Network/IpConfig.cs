using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Network;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Displays the current network configuration.
    /// </summary>
    public sealed class IpConfig : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "ipconfig";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays the current network configuration.";

        /// <inheritdoc/>
        public override string Usage { get; } = "ipconfig";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Network;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var config = NetworkManager.Primary.IPConfig;

            if (config is not null)
            {
                Terminal.InfoLine("IP Address", config.Address.ToString() ?? "None");
                Terminal.InfoLine("Subnet", config.SubnetMask.ToString() ?? "None");
                Terminal.InfoLine("Gateway", config.DefaultGateway.ToString() ?? "None");
            }
            else
            {
                Terminal.WriteLine("Failed to get IP config");
                return CommandReturnCode.GeneralFailure;
            }

            return CommandReturnCode.Success;
        }
    }
}
