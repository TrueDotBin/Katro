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
            Logger.Global.Success("Katro started successfully.");
        }

        protected override void Run()
        {
            
        }
    }
}
