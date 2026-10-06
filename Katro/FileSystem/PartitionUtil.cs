using Cosmos.Kernel.HAL.Interfaces.Devices;
using Cosmos.Kernel.System.Storage;
using Katro.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Utility class for managing partitions on a disk.
    /// </summary>
    public static class PartitionUtil
    {
        private const ulong OneMiB = 1024UL * 1024UL;
        private const ulong GptTail = 34;

        private static ulong AlignUp(ulong value, ulong alignment)
            => (value + alignment - 1) / alignment * alignment;

        /// <summary>
        /// Creates a partition, filling the whole disk up.
        /// </summary>
        /// <param name="device">The block device to create a partition on.</param>
        /// <param name="gptType">The type of the partition.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool CreateFull(IBlockDevice device, Guid? gptType = null)
        {
            ulong startByte = AlignUp(OneMiB, device.BlockSize);
            ulong lastByte = (device.BlockCount - GptTail) * device.BlockSize;

            if (lastByte <= startByte)
                return false;

            return Create(device, lastByte - startByte, gptType);
        }

        /// <summary>
        /// Creates a partition.
        /// </summary>
        /// <param name="device">The block device to create a partition on.</param>
        /// <param name="size">The size of the new partition, in bytes.</param>
        /// <param name="gptType">The type of the partition.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool Create(IBlockDevice device, ulong size, Guid? gptType = null)
        {
            gptType ??= Gpt.BasicDataPartitionType;

            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
                return false;

            if (size > device.BlockCount * device.BlockSize)
                return false;

            var aligned = AlignUp(size, device.BlockSize);
            var startByte = OneMiB;

            var startSector = startByte / device.BlockSize;
            var sectorCount = aligned / device.BlockSize;

            ulong lastUsable = device.BlockCount - GptTail;
            if (startSector + sectorCount > lastUsable)
                return false;

            Logger.Info($"Aligned size: {aligned}");
            Logger.Info($"Start byte: {startByte}");
            Logger.Info($"Start sector: {startSector}");
            Logger.Info($"Sector count: {sectorCount}");
            Logger.Info($"Last usable: {lastUsable}");
            Logger.Info($"Partitions so far: {StorageManager.Partitions.Count}");

            var result = PartitionManager.Create(device, startSector, sectorCount, 0x0C, gptType.Value);
            StorageManager.RescanPartitions(device);

            Logger.Info($"Partitions after create: {StorageManager.Partitions.Count}");

            return result;
        }
    }
}
