using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Utility for managing partitions.
    /// </summary>
    public static class PartitionUtility
    {
        private const ulong OneMiB = 1024UL * 1024UL;
        private const ulong GptTail = 34;

        private static ulong AlignUp(ulong value, ulong alignment)
            => (value + alignment - 1) / alignment * alignment;

        /// <summary>
        /// Creates a partition that fills the whole disk up.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool CreateFull(IBlockDevice device, MbrSystemId mbrSystemId, Guid gptPartitionType)
        {
            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
                return false;

            if (device.BlockCount <= GptTail)
                return false;

            var startSector = OneMiB / device.BlockSize;
            var lastUsable = device.BlockCount - GptTail;

            if (startSector >= lastUsable)
                return false;

            var sectorCount = lastUsable - startSector;
            return CreateAt(device, startSector, sectorCount, mbrSystemId, gptPartitionType);
        }

        /// <summary>
        /// Creates a sized partition.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <param name="bytes">The size of that partition in bytes.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool CreateSized(IBlockDevice device, ulong bytes, MbrSystemId mbrSystemId, Guid gptPartitionType)
        {
            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
                return false;

            var startSector = OneMiB / device.BlockSize;
            var lastUsable = device.BlockCount - GptTail;
            var sectorCount = AlignUp(bytes, device.BlockSize) / device.BlockSize;

            if (startSector >= lastUsable || sectorCount > lastUsable - startSector)
                return false;

            return CreateAt(device, startSector, sectorCount, mbrSystemId, gptPartitionType);
        }

        /// <summary>
        /// Creates a partition.
        /// </summary>
        /// <param name="device">The block device.</param>
        /// <param name="startSector">The start sector.</param>
        /// <param name="sectorCount">The sector count.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        private static bool CreateAt(IBlockDevice device, ulong startSector, ulong sectorCount,
                                     MbrSystemId mbrSystemId, Guid gptPartitionType)
        {
            if (!PartitionManager.Create(device, startSector, sectorCount, (byte)mbrSystemId, gptPartitionType))
                return false;

            StorageManager.RescanPartitions(device);
            return true;
        }
    }
}
