using Katro.FileSystem;
using Katro.Services;
using Katro.Services.Core;
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
            ServiceHost.StartAll
            (
                new FileSystemService()
            );
        }

        protected override void Run()
        {
            
        }
    }
}
