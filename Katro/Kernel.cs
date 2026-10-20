using Cosmos.Kernel.System.Graphics;
using Katro.Assets;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Lua;
using Katro.Tests;
using Katro.Utils;
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
            Bootstrap.Init();

            if (CmdLineUtils.HasArg("tests"))
            {
                Logger.Info("Running tests");

                TestRunner.FileSystemTests();
            }

            Logger.Ok("Katro booted successfully.");

            if (File.Exists("/katro/bin/ktsh.lua"))
                LuaRunner.RunFile("/katro/bin/ktsh.lua");
            else
                Logger.Error("KTSH was not found");
        }

        protected override void Run()
        {
            // do something else here
        }
    }
}
