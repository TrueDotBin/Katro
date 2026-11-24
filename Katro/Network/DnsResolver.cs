using Cosmos.Kernel.System.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Network
{
    /// <summary>
    /// Resolves the nameserver from a domain.
    /// </summary>
    public static class DnsResolver
    {
        private static bool _initialized;

        /// <summary>
        /// The current DNS client.
        /// </summary>
        public static DnsClient Dns { get; private set; }

        private static Address _cloudflarePrimary;
        private static Address _cloudflareSecondary;

        /// <summary>
        /// Initializes the DNS client.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized)
                return;

            _cloudflarePrimary = new Address4(1, 1, 1, 1);
            _cloudflareSecondary = new Address4(1, 0, 0, 1);

            DnsConfig.Add(_cloudflarePrimary);
            DnsConfig.Add(_cloudflareSecondary);

            Dns = new();

            _initialized = true;
        }

        /// <summary>
        /// Closes the DNS client.
        /// </summary>
        public static void Close()
            => Dns.Dispose();

        /// <summary>
        /// Resolves a nameserver from the provided domain.
        /// </summary>
        /// <param name="domain">The domain name to resolve the nameserver from.</param>
        /// <param name="secondary">Whether to use the secondary DNS server.</param>
        /// <param name="timeout">How long until DNS times out.</param>
        public static Address? Resolve(string domain, bool secondary = false, int timeout = 5000)
        {
            if (secondary)
                Dns.Connect(_cloudflareSecondary);
            else
                Dns.Connect(_cloudflarePrimary);

            Dns.SendQuery(domain);

            var addr = Dns.Receive(timeout);
            return addr;
        }
    }
}
