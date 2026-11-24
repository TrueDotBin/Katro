using Katro.FileSystem;
using Katro.Output;
using Katro.Services;
using Katro.Services.Core;
using Katro.Shell;
using Katro.Users;
using TrueDotBin.CosmosUtils;
using Sys = Cosmos.Kernel.System;

using Org.BouncyCastle.Crypto.Digests;

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
                new NetworkService(),
                new FileSystemService(),
                new UserService(),
                new ConfigService()
            );

            Terminal.AskForUser();

            _shell = new KatroShell
            {
                CurrentDirectory = UserManager.CurrentUser.HomeDirectory
            };

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
