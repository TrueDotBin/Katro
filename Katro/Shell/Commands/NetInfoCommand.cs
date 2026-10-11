using Cosmos.Kernel.System.Network;
using Katro.Enums;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class NetInfoCommand : Command
    {
        public override string Name { get; } = "netinfo";
        public override string[] Aliases { get; } = ["net"];
        public override string Description { get; } = "Prints information about the network.";
        public override string Usage { get; } = "netinfo [options]";

        public override CommandArgument[] Args { get; } =
        [
            new OptionArgument("primary", "p", "Whether to use the primary network device", false)
        ];

        public override int Run()
        {
            if (TryGetOption("primary", out var opt))
            {
                var device = NetworkManager.Primary;

                Console.WriteLine($"Network Device {device.Name}");
                Console.WriteLine($"Is ready: {device.Ready}");
                Console.WriteLine($"Link up: {device.LinkUp}");
                Console.WriteLine($"MAC Address: {device.MacAddress}");

                var ipConfig = device.IPConfig;

                if (ipConfig is null)
                {
                    Logger.Error("Could not fetch IP configuration, was the network initialized?");
                    return (int)CommandReturnCode.GeneralFailure;
                }

                Console.WriteLine($"IP Address: {ipConfig.Address}");
                Console.WriteLine($"Default Gateway: {ipConfig.DefaultGateway}");
                Console.WriteLine($"Subnet Mask: {ipConfig.SubnetMask}");
            }
            else
            {
                var devices = NetworkManager.DeviceCount;

                for (int i = 0; i < devices; i++)
                {
                    var device = NetworkManager.GetAdapter(i);

                    Console.WriteLine($"Network Device {device.Name}");
                    Console.WriteLine($"Is ready: {device.Ready}");
                    Console.WriteLine($"Link up: {device.LinkUp}");
                    Console.WriteLine($"MAC Address: {device.MacAddress}");

                    var ipConfig = device.IPConfig;

                    if (ipConfig is null)
                    {
                        Logger.Error($"Could not fetch IP configuration for device {device.Name}");
                        continue;
                    }

                    Console.WriteLine($"IP Address: {ipConfig.Address}");
                    Console.WriteLine($"Default Gateway: {ipConfig.DefaultGateway}");
                    Console.WriteLine($"Subnet Mask: {ipConfig.SubnetMask}");
                }
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
