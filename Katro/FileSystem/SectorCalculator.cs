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
        private static ulong Ceil(ulong bytes, ulong blockSize)
            => (bytes + blockSize - 1) / blockSize;

        /// <summary>
        /// Converts bytes to a sector count.
        /// </summary>
        /// <param name="blockSize">The size of a logical block, usually 512 or 4096</param>
        /// <param name="bytes">The bytes to convert to sectors.</param>
        /// <returns>The sector count.</returns>
        public static ulong ToSectorCount(ulong blockSize, ulong bytes)
            => Ceil(bytes, blockSize);
    }
}
