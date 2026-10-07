using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Graphics;
using Katro.Extensions;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Lua;
using Katro.Terminal;
using Katro.Utils;
using System;
using Sys = Cosmos.Kernel.System;

namespace Katro
{
    /// <summary>
    /// Main kernel class.
    /// </summary>
    public class Kernel : Sys.Kernel
    {
        /// <summary>
        /// Whether to not use the filesystem.
        /// </summary>
        public static bool DontUseFilesystem { get; private set; }

        protected override void BeforeRun()
        {
            KernelConsole.Default?.SetFontFromResource("Katro.Resources.ZapVga16.psf");

            DontUseFilesystem = CmdLineUtils.HasArg("nofs");
            if (!DontUseFilesystem)
            {
                Logger.Info("Initializing file system");
                FileSystemManager.Init();
                Directory.SetCurrentDirectory("/katro");
            }

            Logger.Info("Testing Lua...");

            var sampleScript = """
                -- Sample Lua script for Katro
                print("Running Lua on Katro!")
                print(_VERSION)

                local a = 5
                local b = 2
                local c = a + b
                print("a="..a)
                print("b="..b)
                print("c=a+b="..c)
                """;

            LuaRunner.RunString(sampleScript);

            Logger.Ok("Lua test finished!");

            Logger.Ok("Katro booted successfully");
        }

        protected override void Run()
        {
            if (!DontUseFilesystem)
            {
                var current = Directory.GetCurrentDirectory();
                Console.Write($"{current} > ");
            }
            else
            {
                Console.Write("Katro > ");
            }

            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            Console.WriteLine($"Input: {input}");
        }
    }
}
