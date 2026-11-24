using Cosmos.Kernel.System.Network;
using Katro.FileSystem;
using Katro.Network;
using Katro.Output;
using Katro.Services.Abstractions;
using System.Net.Sockets;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.Services.Core
{
    /// <summary>
    /// Initializes the network.
    /// </summary>
    public class NetworkService : KernelService
    {
        /// <inheritdoc/>
        public override string Name { get; } = "Network";

        /// <inheritdoc/>
        public override string Id { get; } = "net";

        private static DhcpClient _dchp = new();

        /// <inheritdoc/>
        public override bool Start()
        {
            if (NetworkManager.DeviceCount == 0)
            {
                Logger.Log(LogLevel.Error, "No network devices found!");
                return false;
            }

            Logger.Log(LogLevel.Info, "Information about primary device:");

            Terminal.InfoLine("Name", NetworkManager.Name ?? "None");
            Terminal.InfoLine("MAC Address", NetworkManager.MacAddress?.ToString() ?? "None");
            Terminal.InfoLine("Link Up?", NetworkManager.LinkUp ? "Yes" : "No");
            Terminal.InfoLine("Ready?", NetworkManager.Ready ? "Yes" : "No");

            Logger.Log(LogLevel.Info, "Sending discover packet over DHCP");

            if (_dchp.SendDiscoverPacket() != -1)
            {
                Logger.Log(LogLevel.Info, "Successfully sent discover packet");

                var config = NetworkManager.Primary.IPConfig;
                if (config is not null)
                {
                    Terminal.InfoLine("IP Address", config.Address.ToString() ?? "None");
                    Terminal.InfoLine("Subnet", config.SubnetMask.ToString() ?? "None");
                    Terminal.InfoLine("Gateway", config.DefaultGateway.ToString() ?? "None");
                }
            }
            else
            {
                Logger.Log(LogLevel.Error, "DHCP timed out");
                return false;
            }

            Logger.Log(LogLevel.Info, "Initializing DNS client");
            DnsResolver.Initialize();

            return true;
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            Logger.Log(LogLevel.Info, "Closing DHCP client");
            _dchp.Dispose();

            Logger.Log(LogLevel.Info, "Closing DNS client");
            DnsResolver.Close();
        }
    }
}
