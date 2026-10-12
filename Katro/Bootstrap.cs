using Cosmos.Kernel.System.Graphics;
using Katro.Assets;
using Katro.FileSystem;
using Katro.Logging;

namespace Katro
{
    /// <summary>
    /// Handles kernel initialization.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// The ASCII logo.
        /// </summary>
        public static readonly string[] AsciiLogo =
        {
            "##   ##  #####  ######## ######   ######  ",
            "##  ##  ##   ##    ##    ##   ## ##    ## ",
            "#####   #######    ##    ######  ##    ## ",
            "##  ##  ##   ##    ##    ##   ## ##    ## ",
            "##   ## ##   ##    ##    ##   ##  ######  "
        };

        /// <summary>
        /// Prints the boot logo.
        /// </summary>
        public static void PrintBootLogo()
        {
            foreach (var line in AsciiLogo)
                Console.WriteLine(line);

            Console.WriteLine($"Version {SystemInfo.Version}");
            Console.WriteLine($"Developed by {SystemInfo.Developer}");

            Console.WriteLine();
        }

        /// <summary>
        /// Initializes the kernel
        /// </summary>
        public static void Init()
        {
            AssetManager.Load();
            KernelConsole.Default?.Font = AssetManager.ConsoleFont;

            PrintBootLogo();

            Logger.Info("Initializing file system");
            FileSystemManager.Init();

            Directory.SetCurrentDirectory(FileSystemManager.RootPartitionMountPoint);
        }
    }
}