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

        private static bool PrintInfoAboutDevice(NetworkAdapter device)
        {
            Console.WriteLine($"Network Device {device.Name}");
            Console.WriteLine();
            Console.WriteLine($"Ready: {device.Ready}");
            Console.WriteLine($"Link up: {device.LinkUp}");
            Console.WriteLine($"MAC Address: {device.MacAddress}");

            var ipConfig = device.IPConfig;

            if (ipConfig is null)
            {
                Logger.Error($"Could not fetch IP configuration for device {device.Name}");
                return false;
            }

            Console.WriteLine();
            Console.WriteLine("IP Configuration:");
            Console.WriteLine($"IP Address: {ipConfig.Address}");
            Console.WriteLine($"Default Gateway: {ipConfig.DefaultGateway}");
            Console.WriteLine($"Subnet Mask: {ipConfig.SubnetMask}");

            return true;
        }

        public override int Run()
        {
            if (TryGetOption("primary", out var opt))
            {
                var device = NetworkManager.Primary;
                PrintInfoAboutDevice(device);
            }
            else
            {
                var devices = NetworkManager.DeviceCount;

                for (int i = 0; i < devices; i++)
                {
                    var device = NetworkManager.GetAdapter(i);
                    PrintInfoAboutDevice(device);
                }
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
