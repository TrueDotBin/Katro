using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Helper math operations for file system stuff.
    /// </summary>
    public static class FileSystemMath
    {
        /// <summary>
        /// One mebibyte.
        /// </summary>
        public const ulong OneMiB = 1024UL * 1024UL;

        /// <summary>
        /// The number of reserved sectors at the end of the disk that are excluded from usable partition space.
        /// </summary>
        public const ulong GptTail = 34;

        /// <summary>
        /// Aligns <paramref name="value"/> up to the nearest multiple of <paramref name="alignment"/>.
        /// </summary>
        /// <param name="value">The value to align.</param>
        /// <param name="alignment">The alignment.</param>
        /// <returns>The aligned value.</returns>
        public static ulong AlignUp(ulong value, ulong alignment)
            => (value + alignment - 1) / alignment * alignment;

        /// <summary>
        /// Calculates the number of blocks required to hold <paramref name="bytes"/>.
        /// </summary>
        /// <remarks>
        /// Prefer this over <see cref="Math.Ceiling(double)"/> for sector math.
        /// </remarks>
        /// <param name="bytes">The total size in bytes.</param>
        /// <param name="blockSize">The size of a single block in bytes.</param>
        /// <returns>The number of blocks, rounded up.</returns>
        public static ulong Ceil(ulong bytes, ulong blockSize)
            => (bytes + blockSize - 1) / blockSize;
    }
}
