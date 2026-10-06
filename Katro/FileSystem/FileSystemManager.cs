using Cosmos.Kernel.HAL.Interfaces.Devices;
using Cosmos.Kernel.HAL.Vfs;
using Cosmos.Kernel.System.Filesystems.Ext2;
using Cosmos.Kernel.System.Filesystems.Fat;
using Cosmos.Kernel.System.Storage;
using Cosmos.Kernel.System.Vfs;
using Katro.Constants;
using Katro.Logging;
using Katro.Terminal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Manages the file system.
    /// </summary>
    public static class FileSystemManager
    {
        /// <summary>
        /// Registers the filesystem type.
        /// </summary>
        /// <returns>Whether the filesystem type was registered successfully.</returns>
        public static bool Register()
        {
            var fat = new FatFilesystemType();
            return VfsManager.RegisterFilesystem("fat", fat);
        }

        /// <summary>
        /// Creates the root partition.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <returns>Whether the partition was created and formatted successfully.</returns>
        public static bool CreateRootPartition(IBlockDevice device)
        {
            var formatOpts = new FatFormatOptions()
            {
                VolumeLabel = "KATRO",
                Type = FatType.Fat32
            };

            Logger.Info($"Volume label: {formatOpts.VolumeLabel}");

            var createSuccess = PartitionUtil.CreateFull(device);
            var formatSuccess = VfsManager.TryFormat("fat", StorageManager.Partitions[0], formatOpts);

            if (!createSuccess)
                Logger.Error("The partition wasn't created successfully");

            if (!formatSuccess)
                Logger.Error("The partition wasn't formatted successfully");

            return createSuccess && formatSuccess;
        }

        /// <summary>
        /// Initializes the file system.
        /// </summary>
        public static void Init()
        {
            Logger.Info("Registering FAT file system type");

            if (!Register())
            {
                ErrorScreen.Show("Failed to register FAT file system type", ErrorCodes.FailedToRegisterFileSystem);
                return;
            }

            Logger.Info("Finding primary disk");
            var primary = StorageManager.PrimaryDevice;

            if (primary is null)
            {
                ErrorScreen.Show("No disk", ErrorCodes.NoDisk);
                return;
            }

            Logger.Info("Found primary disk:");
            Logger.Info($"Block count: {primary.BlockCount}");
            Logger.Info($"Block size: {primary.BlockSize}");
            Logger.Info($"Total size in bytes: {primary.BlockCount * primary.BlockSize}");

            if (!Gpt.IsGpt(primary) && !Mbr.IsMbr(primary))
            {
                Logger.Warn("No partition table found, creating GPT partition table");
                Gpt.Create(primary);
            }

            var parsed = Gpt.Parse(primary);
            Logger.Info($"Found {parsed.Count} entries");

            foreach (var entry in parsed)
            {
                Logger.Info($"GUID: {entry.PartitionGuid}");
                Logger.Info($"Sector count: {entry.SectorCount}");
                Logger.Info($"Start sector: {entry.StartSector}");
                Logger.Info($"Partition type: {entry.PartitionType}");
            }

            if (StorageManager.Partitions.Count == 0)
            {
                Logger.Warn("No partitions found");
                Logger.Info("Creating root partition");
                if (!CreateRootPartition(primary))
                {
                    ErrorScreen.Show("Failed to create root partition", ErrorCodes.FailedToCreateRootPartition);
                    return;
                }
            }

            if (StorageManager.Partitions.Count == 0)
            {
                ErrorScreen.Show("No partitions after create", ErrorCodes.NoPartitions);
                return;
            }

            if (VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/katro", out VfsManager.VfsMount? mount))
                Logger.Ok($"Mounted {mount.Source} -> {mount.MountPoint}");
            else
                ErrorScreen.Show("Failed to mount root partition", ErrorCodes.FailedToMountRootPartition);
        }
    }
}
