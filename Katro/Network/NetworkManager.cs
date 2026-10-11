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
        private static CNetwork.DnsClient s_dns = new();
        private static CNetwork.Address s_nameserver = new CNetwork.Address4(1, 1, 1, 1);

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

            CNetwork.DnsConfig.Add(s_nameserver);
        }

        /// <summary>
        /// Resolves an address of a domain name.
        /// </summary>
        /// <param name="domainName">The domain name to resolve.</param>
        /// <param name="address">The address of the server.</param>
        /// <returns>The address of the server.</returns>
        public static bool TryDnsResolve(string domainName, out CNetwork.Address? address)
        {
            s_dns.Connect(s_nameserver);
            s_dns.SendQuery(domainName);

            var success = true;
            var addr = s_dns.Receive(5000);

            if (addr is null)
            {
                Logger.Error($"DNS timed out or {domainName} is not a valid domain name.");

                address = null;
                success = false;
            }

            address = addr;
            s_dns.Close();

            return success;
        }
    }
}
