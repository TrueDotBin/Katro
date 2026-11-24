using Cosmos.Kernel.System.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Network
{
    /// <summary>
    /// Pings a server.
    /// </summary>
    public static class Pinger
    {
        private static bool _initialized;

        /// <summary>
        /// The current ICMP client.
        /// </summary>
        public static IcmpClient Icmp { get; private set; }

        /// <summary>
        /// Initializes the ICMP client.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized)
                return;

            Icmp = new();

            _initialized = true;
        }

        /// <summary>
        /// Closes the ICMP client.
        /// </summary>
        public static void Close()
            => Icmp.Dispose();

        /// <summary>
        /// Pings a server and returns the result.
        /// </summary>
        /// <param name="address">The address that should be pinged, use <see cref="DnsResolver.Resolve(string, bool, int)"/> to get one from a domain.</param>
        /// <param name="timeout">How long until ICMP times out.</param>
        public static PingResult Ping(Address address, int timeout = 5000)
        {
            Icmp.Connect(address);
            Icmp.SendEcho();

            var endpoint = new EndPoint(Address6.Zero, 0);
            var elapsed = Icmp.Receive(ref endpoint, timeout);
            
            var ping = new PingResult(elapsed);

            return ping;
        }
    }
}
