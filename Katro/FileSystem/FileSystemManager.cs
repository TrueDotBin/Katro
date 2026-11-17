using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.FileSystem;
using Cosmos.Kernel.System.FileSystem.Ext2;
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

            if (!createSuccess)
            {
                Logger.Log(LogLevel.Error, "Failed to create partition");
                return false;
            }

            if (partition == null)
                return false;

            var formatSuccess = VfsManager.TryFormat(FileSystemDriverNames.FatDriverName, partition, formatOpts);

            if (!formatSuccess)
            {
                Logger.Log(LogLevel.Error, "Failed to format partition");
                return false;
            }

            return createSuccess && formatSuccess;
        }

        /// <summary>
        /// Registers the file system drivers.
        /// </summary>
        public static bool RegisterDrivers()
        {
            var fat = new FatFileSystemType();
            var fatSuccess = VfsManager.RegisterFileSystem(FileSystemDriverNames.FatDriverName, fat);

            var ext = new Ext2FileSystemType();
            var extSuccess = VfsManager.RegisterFileSystem(FileSystemDriverNames.ExtDriverName, ext);

            if (!fatSuccess)
                Logger.Log(LogLevel.Error, "Failed to register FAT driver");

            if (!extSuccess)
                Logger.Log(LogLevel.Error, "Failed to register ext2 driver");

            return fatSuccess || extSuccess; 
        }

        /// <summary>
        /// Initializes the file system.
        /// </summary>
        public static void Initialize()
        {
            IBlockDevice AskForDevice()
            {
                var devices = StorageManager.Devices;
                var writableDevices = devices.Where(d =>
                {
                    if (d is IRemovableMedia removable && removable.IsReadOnly)
                        return false;

                    return true;
                });

                var writableDeviceNames = writableDevices.Select(d => d.Name);
                var index = Terminal.Select("Select a device:", writableDeviceNames);
                return devices[index];
            }

            Partition AskForPartition(string hostName, IEnumerable<Partition> partitions)
            {
                var partitionNames = partitions.Select(p => $"{p.Name} (start={p.StartSector}, blockSize={p.BlockSize}, blocks={p.BlockCount})");
                var index = Terminal.Select($"Select a partition on {hostName}:", partitionNames);
                return partitions.ElementAt(index);
            }

            Logger.Log(LogLevel.Info, $"Registering file system drivers");

            if (!RegisterDrivers())
            {
                Logger.Log(LogLevel.Error, "Failed to register drivers");
                return;
            }

            Logger.Log(LogLevel.Info, "Finding primary disk");

            var device = AskForDevice();

            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
            {
                Logger.Log(LogLevel.Warning, "No partition table found, creating a GPT partition table");

                Gpt.Create(device);
            }

            Partition? rootPartition;

            var partitions = StorageManager.GetPartitions(device);
            if (partitions.Count == 0)
            {
                Logger.Log(LogLevel.Info, "Creating and formatting root partition");

                if (!CreateRootPartition(device, out rootPartition))
                {
                    Logger.Log(LogLevel.Error, "Failed to create root partition");
                    return;
                }
            }
            else
            {
                rootPartition = AskForPartition(device.Name, partitions);
            }

            if (rootPartition is null)
            {
                Logger.Log(LogLevel.Error, "Root partition not found");
                return;
            }

            Logger.Log(LogLevel.Info, $"Mounting \"{rootPartition.Name}\" to {MountPoints.RootPartition}");

            if (VfsManager.TryMount(FileSystemDriverNames.FatDriverName, rootPartition, MountFlags.None, MountPoints.RootPartition, out var mount))
                Initialized = true;
            else
                Logger.Log(LogLevel.Error, "Failed to mount partition");
        }
    }
}
