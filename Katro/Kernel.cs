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
            Logger.Ok("Katro booted successfully");
        }

        protected override void Run()
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            Console.WriteLine($"Input: {input}");
        }
    }
}
