using Cosmos.Kernel.System.FileSystem;
using Katro.Configuration;
using Katro.Constants.FileSystem;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.FileSystem
{
    /// <summary>
    /// Displays information about the file system.
    /// </summary>
    public sealed class FsStat : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "fsstat";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays information about the file system.";

        /// <inheritdoc/>
        public override string Usage { get; } = "fsstat [--nerds]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        private static void PrintInfoLine(string label, object value)
        {
            var startColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{label,-20}");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ForegroundColor = startColor;
        }

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            if (!VfsManager.TryStatFs(MountPoints.RootPartition, out var stat))
                return CommandReturnCode.GeneralFailure;

            var nerdMode = ctx.HasFlag("nerds");

            if (nerdMode)
            {
                Console.WriteLine("===== File System Info =====");

                PrintInfoLine("Available Blocks", stat.AvailableBlocks);
                PrintInfoLine("Free Blocks", stat.FreeBlocks);
                PrintInfoLine("Block Size", stat.BlockSize);
                PrintInfoLine("Blocks", stat.Blocks);

                PrintInfoLine("Fragment Size", stat.FragmentSize);

                PrintInfoLine("Free Inodes", stat.FreeInodes);
                PrintInfoLine("Inodes", stat.Inodes);

                PrintInfoLine("Max Name Length", stat.MaxNameLength);
            }
            else
            {
                Console.WriteLine("===== File System Info =====");

                var totalSize = stat.Blocks * stat.BlockSize;
                var freeSpace = stat.FreeBlocks * stat.BlockSize;

                var usedBlocks = stat.Blocks - stat.FreeBlocks;
                var usedPercent = (double)usedBlocks / stat.Blocks * 100;

                var usedSpace = usedBlocks * stat.BlockSize;

                PrintInfoLine("Total Size", FileSizeFormatter.FormatSize(totalSize));
                PrintInfoLine("Free Space", FileSizeFormatter.FormatSize(freeSpace));
                PrintInfoLine("Used Space", FileSizeFormatter.FormatSize(usedSpace));
                PrintInfoLine("Usage", $"{usedPercent:F1}%");
            }

            return CommandReturnCode.Success;
        }
    }
}
