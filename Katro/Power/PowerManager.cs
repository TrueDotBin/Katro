using Katro.Configuration;
using Katro.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Power
{
    /// <summary>
    /// Safely shuts down/reboots the system.
    /// </summary>
    public static class PowerManager
    {
        private static void Cleanup()
        {
            Console.WriteLine("See you next time!");
            ServiceHost.StopAll();
        }

        /// <summary>
        /// Turns off the device.
        /// </summary>
        public static void Shutdown()
        {
            Cleanup();
            Cosmos.Kernel.System.Power.Shutdown();
        }

        /// <summary>
        /// Turns off the device, and then turns it back on.
        /// </summary>
        public static void Reboot()
        {
            Cleanup();
            Cosmos.Kernel.System.Power.Reboot();
        }
    }
}
