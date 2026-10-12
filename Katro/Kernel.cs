using Cosmos.Kernel.System.Graphics;
using Katro.Assets;
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
            AssetManager.Load();
            KernelConsole.Default?.Font = AssetManager.ConsoleFont;

            Console.WriteLine("Katro booted successfully.");
        }

        protected override void Run()
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                return;

            Console.WriteLine(input);
        }
    }
}
