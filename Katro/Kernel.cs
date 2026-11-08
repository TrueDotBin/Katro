using Katro.FileSystem;
using TrueDotBin.CosmosUtils;
using TrueDotBin.CosmosUtils.Logging;
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
            FileSystemManager.Initialize();
            if (!FileSystemManager.Initialized)
            {
                Logger.Global.Error("File system failed to initialize");

                Stop();
                return;
            }

            Logger.Global.Success("Katro started successfully.");
        }

        protected override void Run()
        {
            
        }
    }
}
