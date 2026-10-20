using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.FileSystem;
using Cosmos.Kernel.System.FileSystem.Fat;
using Cosmos.Kernel.System.Storage;
using Katro.Logging;
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
        /// Whether the file system has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// The mount point of the root partition.
        /// </summary>
        public const string RootPartitionMountPoint = "/katro";

        /// <summary>
        /// The name of the FAT driver.
        /// </summary>
        public const string FatDriverName = "fat";

        /// <summary>
        /// Registers the FAT driver.
        /// </summary>
        /// <returns>Whether the driver was registered successfully.</returns>
        public static bool RegisterFatDriver()
        {
            var fat = new FatFileSystemType();
            return VfsManager.RegisterFileSystem(FatDriverName, fat);
        }

        /// <summary>
        /// Tries to create and format the root partition.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <returns>Whether the partition was created and formatted successfully.</returns>
        public static bool CreateAndFormatRootPartition(IBlockDevice device)
        {
            var formatOpts = new FatFormatOptions()
            {
                VolumeLabel = "KATRO",
                Type = FatType.Fat32
            };

            var createSuccess = PartitionUtility.CreateFull(device, MbrSystemId.Fat32Lba, Gpt.BasicDataPartitionType);
            var formatSuccess = VfsManager.TryFormat(FatDriverName, StorageManager.Partitions[0], formatOpts);

            return createSuccess && formatSuccess;
        }

        /// <summary>
        /// Creates, formats and mounts the necessary partitions.
        /// </summary>
        public static void Init()
        {
            if (!RegisterFatDriver())
            {
                Logger.Error("Failed to register FAT driver");
                return;
            }

            var primary = StorageManager.PrimaryDevice;

            if (primary is null)
            {
                Logger.Error("No disk");
                return;
            }

            if (!Gpt.IsGpt(primary) && !Mbr.IsMbr(primary))
            {
                Logger.Info("No partition table found.");

                Gpt.Create(primary);
            }

            if (StorageManager.Partitions.Count == 0)
            {
                Logger.Warn("No partitions found, creating root partition");

                if (!CreateAndFormatRootPartition(primary))
                {
                    Logger.Error("Failed to create and format root partition");
                    return;
                }
            }

            if (VfsManager.TryMount(FatDriverName, StorageManager.Partitions[0], MountFlags.None, RootPartitionMountPoint, out var mnt))
            {
                Initialized = true;
                Logger.Ok($"Mounted partition {mnt.Source} -> {mnt.MountPoint}");
            }
            else
                Logger.Error($"Failed to mount partition");
        }
    }
}
