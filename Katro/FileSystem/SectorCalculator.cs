using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Calculates the sector count from bytes.
    /// </summary>
    public static class SectorCalculator
    {
        /// <summary>
        /// Converts bytes to a sector count.
        /// </summary>
        /// <param name="blockSize">The size of a logical block, usually 512 or 4096</param>
        /// <param name="bytes">The bytes to convert to sectors.</param>
        /// <returns>The sector count.</returns>
        public static ulong ToSectorCount(ulong blockSize, ulong bytes)
            => FileSystemMath.Ceil(bytes, blockSize);

        /// <summary>
        /// Determines the next start sector for the block device.
        /// </summary>
        /// <param name="device">The block device.</param>
        /// <returns>The start sector.</returns>
        public static ulong DetermineStartSector(IBlockDevice device)
        {
            var alignSectors = FileSystemMath.OneMiB / device.BlockSize;
            var startSector = alignSectors;

            foreach (var existing in StorageManager.GetPartitions(device))
            {
                var end = existing.StartSector + existing.BlockCount;

                if (end > startSector)
                    startSector = FileSystemMath.AlignUp(end, alignSectors);
            }

            return startSector;
        }
    }
}
