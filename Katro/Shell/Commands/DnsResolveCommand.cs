using Katro.Enums;
using Katro.Logging;
using Katro.Network;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class DnsResolveCommand : Command
    {
        public override string Name { get; } = "dnsresolve";
        public override string[] Aliases { get; } = ["dns"];
        public override string Description { get; } = "Resolves a domain name's server.";
        public override string Usage { get; } = "dnsresolve <domainName>";

        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("domainName", 0, "The domain name to resolve")
        ];

        public override int Run()
        {
            if (!TryGetPositional(0, out var domainNameArg))
                return (int)CommandReturnCode.NoArguments;

            var domainName = domainNameArg.Value?.ToString();
            
            if (string.IsNullOrEmpty(domainName))
                return (int)CommandReturnCode.BadArgument;

            if (NetworkManager.TryDnsResolve(domainName, out var addr))
            {
                Console.WriteLine($"{domainName} -> {addr}");
                return (int)CommandReturnCode.Success;
            }
            else
            {
                Logger.Error($"Failed to resolve {domainName}");
                return (int)CommandReturnCode.GeneralFailure;
            }
        }
    }
}
