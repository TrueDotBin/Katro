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

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var nerdMode = ctx.HasFlag("nerds");

            foreach (var mount in VfsManager.Mounts)
            {
                if (!VfsManager.TryStatFs(mount.MountPoint, out var stat))
                {
                    Console.WriteLine($"Failed to stat {mount.MountPoint}");
                    continue;
                }

                Terminal.Heading($"File System Info for {mount.MountPoint}");

                if (nerdMode)
                {
                    Terminal.InfoLine("Available Blocks", stat.AvailableBlocks);
                    Terminal.InfoLine("Free Blocks", stat.FreeBlocks);
                    Terminal.InfoLine("Block Size", stat.BlockSize);
                    Terminal.InfoLine("Blocks", stat.Blocks);

                    Terminal.InfoLine("Fragment Size", stat.FragmentSize);

                    Terminal.InfoLine("Free Inodes", stat.FreeInodes);
                    Terminal.InfoLine("Inodes", stat.Inodes);

                    Terminal.InfoLine("Max Name Length", stat.MaxNameLength);
                }
                else
                {
                    var totalSize = stat.Blocks * stat.BlockSize;
                    var freeSpace = stat.FreeBlocks * stat.BlockSize;

                    var usedBlocks = stat.Blocks - stat.FreeBlocks;
                    var usedPercent = (double)usedBlocks / stat.Blocks * 100;

                    var usedSpace = usedBlocks * stat.BlockSize;

                    Terminal.InfoLine("Total Size", FileSizeFormatter.FormatSize(totalSize));
                    Terminal.InfoLine("Free Space", FileSizeFormatter.FormatSize(freeSpace));
                    Terminal.InfoLine("Used Space", FileSizeFormatter.FormatSize(usedSpace));
                    Terminal.InfoLine("Usage", $"{usedPercent:F1}%");
                }
            }

            return CommandReturnCode.Success;
        }
    }
}
