using Cosmos.Kernel.System.FileSystem;
using Katro.Configuration;
using Katro.Constants.FileSystem;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
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

        private static void DisplayFsStatNerds(VfsMount mount, VfsStatFs stat, bool printSeparator = false)
        {
            if (printSeparator)
                Terminal.WriteLine();

            Terminal.Heading("Mount");
            Terminal.InfoLine("Mounted On", mount.MountPoint);
            Terminal.InfoLine("Mount Name", mount.Name);
            Terminal.InfoLine("Mount Source", mount.Source);
            Terminal.InfoLine("Read-Only?", mount.IsReadOnly ? "Yes" : "No");
            Terminal.InfoLine("Mount Flags", mount.Flags.ToString());

            if (mount.Partition is not null)
            {
                Terminal.WriteLine();

                Terminal.Heading("Mount Partition");
                Terminal.InfoLine("Partition Name", mount.Partition.Name);

                Terminal.InfoLine("Partition Start Sector", mount.Partition.StartSector);
                Terminal.InfoLine("Partition Sector Count", mount.Partition.StartSector);

                Terminal.InfoLine("Partition Block Count", mount.Partition.BlockCount);
                Terminal.InfoLine("Partition Block Size", mount.Partition.BlockSize);
            }

            Terminal.WriteLine();

            Terminal.Heading("Blocks");
            Terminal.InfoLine("Available Blocks", stat.AvailableBlocks);
            Terminal.InfoLine("Free Blocks", stat.FreeBlocks);
            Terminal.InfoLine("Block Size", stat.BlockSize);
            Terminal.InfoLine("Blocks", stat.Blocks);
            Terminal.WriteLine();

            Terminal.Heading("Fragments");
            Terminal.InfoLine("Fragment Size", stat.FragmentSize);
            Terminal.WriteLine();

            Terminal.Heading("Inodes");
            Terminal.InfoLine("Free Inodes", stat.FreeInodes);
            Terminal.InfoLine("Inodes", stat.Inodes);
            Terminal.WriteLine();

            Terminal.Heading("Other");
            Terminal.InfoLine("Max Name Length", stat.MaxNameLength);
        }

        private static void DisplayFsStatShort(VfsMount mount, VfsStatFs stat, bool printSeparator = false)
        {
            if (printSeparator)
                Terminal.WriteLine();

            var totalSize = stat.Blocks * stat.BlockSize;
            var freeSpace = stat.FreeBlocks * stat.BlockSize;

            var usedBlocks = stat.Blocks - stat.FreeBlocks;
            var usedPercent = (double)usedBlocks / stat.Blocks * 100;

            var usedSpace = usedBlocks * stat.BlockSize;

            Terminal.Heading("Mount");
            Terminal.InfoLine("Mounted On", mount.MountPoint);
            Terminal.InfoLine("Mount Name", mount.Name);
            Terminal.WriteLine();

            Terminal.Heading("Sizes");
            Terminal.InfoLine("Total Size", FileSizeFormatter.FormatSize(totalSize));
            Terminal.InfoLine("Free Space", FileSizeFormatter.FormatSize(freeSpace));
            Terminal.InfoLine("Used Space", FileSizeFormatter.FormatSize(usedSpace));
            Terminal.InfoLine("Usage", $"{usedPercent:F1}%");
        }

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var nerdMode = ctx.HasFlag("nerds");

            var mounts = VfsManager.Mounts;
            Terminal.WriteLine($"{mounts.Count} mount(s)");
            Terminal.WriteLine();

            foreach (var mount in mounts)
            {
                if (!VfsManager.TryStatFs(mount.MountPoint, out var stat))
                {
                    Terminal.WriteLine($"Failed to stat {mount.MountPoint}");
                    continue;
                }

                var showSeparator = mount != mounts[0];

                if (nerdMode)
                    DisplayFsStatNerds(mount, stat, showSeparator);
                else
                    DisplayFsStatShort(mount, stat, showSeparator);
            }

            return CommandReturnCode.Success;
        }
    }
}
