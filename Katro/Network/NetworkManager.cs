using System.Net;
using System.Net.Sockets;
using System.Text;
using CNetwork = Cosmos.Kernel.System.Network;
using Cosmos.Kernel.System.Timers;
using Katro.Logging;

namespace Katro.Network
{
    /// <summary>
    /// Manages the network.
    /// </summary>
    public static class NetworkManager
    {
        /// <summary>
        /// The current IP address.
        /// </summary>
        public static CNetwork.Address? IpAddress { get; private set; }

        private static CNetwork.DhcpClient s_dhcp = new();

        private static void ResolveIpAddress()
        {
            if (s_dhcp.SendDiscoverPacket() != -1)
            {
                var config = CNetwork.NetworkManager.Primary.IPConfig;

                if (config is not null)
                {
                    IpAddress = config.Address;
                    Logger.Info($"IP Address: {IpAddress}");
                }
            }
            else
            {
                Logger.Error("DCHP timed out");
            }
        }

        /// <summary>
        /// Initializes the network.
        /// </summary>
        public static void Init()
        {
            if (CNetwork.NetworkManager.DeviceCount == 0)
            {
                Logger.Error("No network devices found");
                return;
            }

            var networkDevices = CNetwork.NetworkManager.DeviceCount;
            Logger.Info($"Found {networkDevices} network device(s)");

            ResolveIpAddress();
        }
    }
}
