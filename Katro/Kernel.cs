using Katro.FileSystem;
using Katro.Services;
using Katro.Services.Core;
using Katro.Shell;
using Sys = Cosmos.Kernel.System;

namespace Katro
{
    /// <summary>
    /// Main kernel class.
    /// </summary>
    public class Kernel : Sys.Kernel
    {
        private KatroShell _shell;

        protected override void BeforeRun()
        {
            ServiceHost.StartAll
            (
                new ConsoleFontService(),
                new FileSystemService()
            );

            _shell = new KatroShell();
            _shell.Initialize();
        }

        protected override void Run()
        {
            _shell.Run();
        }
    }
}
