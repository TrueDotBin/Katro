using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.FileSystem;
using Cosmos.Kernel.System.FileSystem.Fat;
using Cosmos.Kernel.System.Storage;
using Katro.Constants.FileSystem;
using TrueDotBin.CosmosUtils.FileSystem;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.FileSystem
{
    /// <summary>
    /// Manages the file system.
    /// </summary>
    public static class FileSystemManager
    {
        /// <summary>
        /// The logger for the file system.
        /// </summary>
        public static readonly Logger Logger = new("File System");

        /// <summary>
        /// Whether the file system has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }


        /// <summary>
        /// Creates and formats the root partition.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <param name="partition">The output partition.</param>
        /// <returns>Whether the partition was created and formatted successfully.</returns>
        public static bool CreateRootPartition(IBlockDevice device, out Partition? partition)
        {
            var formatOpts = new FatFormatOptions()
            {
                VolumeLabel = "KATRO",
                Type = FatType.Fat32
            };

            var createSuccess = PartitionUtility.CreateFull(device, MbrSystemId.Fat32Lba,
                Gpt.BasicDataPartitionType, out partition);

            if (partition == null)
                return false;

            var formatSuccess = VfsManager.TryFormat(FileSystemDriverNames.FatDriverName, partition, formatOpts);

            return createSuccess && formatSuccess;
        }

        /// <summary>
        /// Registers the FAT driver to the file system.
        /// </summary>
        public static bool RegisterFatDriver()
        {
            var fat = new FatFileSystemType();
            return VfsManager.RegisterFileSystem(FileSystemDriverNames.FatDriverName, fat);
        }

        /// <summary>
        /// Initializes the file system.
        /// </summary>
        public static void Initialize()
        {
            Logger.Log(LogLevel.Info, $"Registering FAT driver as \"{FileSystemDriverNames.FatDriverName}\"");

            if (!RegisterFatDriver())
            {
                Logger.Log(LogLevel.Error, "Failed to register FAT driver");
                return;
            }

            Logger.Log(LogLevel.Info, "Finding primary disk");

            var primary = StorageManager.PrimaryDevice;
            if (primary is null)
            {
                Logger.Log(LogLevel.Error, "No primary disk found");
                return;
            }

            if (!Gpt.IsGpt(primary) && !Mbr.IsMbr(primary))
            {
                Logger.Log(LogLevel.Warning, "No partition table found, creating a GPT partition table");
                Gpt.Create(primary);
            }

            Partition? rootPartition;

            if (StorageManager.Partitions.Count == 0)
            {
                Logger.Log(LogLevel.Info, "Creating and formatting root partition");

                if (!CreateRootPartition(primary, out rootPartition))
                {
                    Logger.Log(LogLevel.Error, "Failed to create root partition");
                    return;
                }
            }
            else
            {
                Logger.Log(LogLevel.Info, "Finding root partition");
                rootPartition = StorageManager.Partitions[0];
            }

            if (rootPartition is null)
            {
                Logger.Log(LogLevel.Error, "Root partition not found");
                return;
            }

            Logger.Log(LogLevel.Info, $"Mounting root partition to {MountPoints.RootPartition}");

            if (!VfsManager.TryMount(FileSystemDriverNames.FatDriverName, rootPartition, MountFlags.None, MountPoints.RootPartition, out var mount))
                Initialized = true;
            else
                Logger.Log(LogLevel.Error, "Failed to mount root partition");
        }
    }
}
