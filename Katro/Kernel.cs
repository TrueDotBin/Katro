using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Graphics;
using Katro.Extensions;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Terminal;
using System;
using Sys = Cosmos.Kernel.System;

namespace Katro
{
    /// <summary>
    /// Main kernel class.
    /// </summary>
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            KernelConsole.Default?.SetFontFromResource("Katro.Resources.ZapVga16.psf");

            Logger.Info("Initializing file system");
            FileSystemManager.Init();

            Logger.Ok("Katro booted successfully");

            Directory.SetCurrentDirectory("/katro");
        }

        protected override void Run()
        {
            var current = Directory.GetCurrentDirectory();
            Console.Write($"{current} > ");

            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            Console.WriteLine($"Input: {input}");
        }
    }
}
