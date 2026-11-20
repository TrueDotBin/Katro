using Katro.FileSystem;
using Katro.Output;
using Katro.Services;
using Katro.Services.Core;
using Katro.Shell;
using TrueDotBin.CosmosUtils;
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
            Defaults.DefaultOutputFormat = new()
            {
                ShowSource = true
            };

            ServiceHost.StartAll
            (
                new ConsoleFontService(),
                new FileSystemService(),
                new ConfigService()
            );

            _shell = new KatroShell();
            _shell.Initialize();
        }

        protected override void Run()
        {
            try
            {
                _shell.Run();
            }
            catch (Exception ex)
            {
                Terminal.CSharpException(ex);
            }
        }

        protected override void AfterRun()
        {
            ServiceHost.StopAll();
        }
    }
}
