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

        private static void CreateSystemFiles()
        {
            Logger.Info("Creating system files");

            FileWriter.WriteBytes("/katro/bin/ktsh.lua", AssetManager.KatroShell);

            FileWriter.WriteBytes("/katro/lib/tokenizer.lua", AssetManager.LibTokenizer);
            FileWriter.WriteBytes("/katro/lib/shell/commands.lua", AssetManager.LibCommands);
        }

        private static void CreateSystemDirectories()
        {
            Logger.Info("Creating system directories");

            Directory.CreateDirectory("/katro/bin");

            Directory.CreateDirectory("/katro/lib");
            Directory.CreateDirectory("/katro/lib/shell");
        }

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

            CreateSystemDirectories();
            CreateSystemFiles();

            Directory.SetCurrentDirectory(FileSystemManager.RootPartitionMountPoint);
        }
    }
}