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
        /// <summary>
        /// Creates a partition that fills the whole disk up.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <param name="partition">The partition that was created.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool CreateFull(IBlockDevice device, MbrSystemId mbrSystemId, Guid gptPartitionType, out Partition? partition)
        {
            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
            {
                partition = null;
                return false;
            }                

            if (device.BlockCount <= FileSystemMath.GptTail)
            {
                partition = null;
                return false;
            }

            var startSector = SectorCalculator.DetermineStartSector(device);
            var lastUsable = device.BlockCount - FileSystemMath.GptTail;

            if (startSector >= lastUsable)
            {
                partition = null;
                return false;
            }

            var sectorCount = lastUsable - startSector;
            return CreateAt(device, startSector, sectorCount, mbrSystemId, gptPartitionType, out partition);
        }

        /// <summary>
        /// Creates a sized partition.
        /// </summary>
        /// <param name="device">The block device to create the partition on.</param>
        /// <param name="bytes">The size of that partition in bytes.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <param name="partition">The partition that was created.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        public static bool CreateSized(IBlockDevice device, ulong bytes, MbrSystemId mbrSystemId,
            Guid gptPartitionType, out Partition? partition)
        {
            if (!Gpt.IsGpt(device) && !Mbr.IsMbr(device))
            {
                partition = null;
                return false;
            }

            var startSector = SectorCalculator.DetermineStartSector(device);
            var lastUsable = device.BlockCount - FileSystemMath.GptTail;
            var sectorCount = FileSystemMath.AlignUp(bytes, device.BlockSize) / device.BlockSize;

            if (startSector >= lastUsable || sectorCount > lastUsable - startSector)
            {
                partition = null;
                return false;
            }

            return CreateAt(device, startSector, sectorCount, mbrSystemId, gptPartitionType, out partition);
        }

        /// <summary>
        /// Creates a partition.
        /// </summary>
        /// <param name="device">The block device.</param>
        /// <param name="startSector">The start sector.</param>
        /// <param name="sectorCount">The sector count.</param>
        /// <param name="mbrSystemId">The MBR system ID.</param>
        /// <param name="gptPartitionType">The GPT partition type.</param>
        /// <param name="partition">The partition that was created.</param>
        /// <returns>Whether the partition was created successfully.</returns>
        private static bool CreateAt(IBlockDevice device, ulong startSector, ulong sectorCount,
                                     MbrSystemId mbrSystemId, Guid gptPartitionType, out Partition? partition)
        {
            var part = new Partition(device, startSector, sectorCount, "Partition");

            if (!PartitionManager.Create(device, part.StartSector, sectorCount, (byte)mbrSystemId, gptPartitionType))
            {
                partition = null;
                return false;
            }

            partition = part;
            StorageManager.RescanPartitions(device);

            return true;
        }
    }
}
