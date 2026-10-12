using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// MBR system IDs.
    /// </summary>
    public enum MbrSystemId : byte
    {
        /// <summary>
        /// Unused partition table entry.
        /// </summary>
        Empty = 0x00,

        /// <summary>
        /// Generic ID for Linux filesystems (e.g., ext2/3/4)
        /// </summary>
        Linux = 0x83,

        /// <summary>
        /// Used for swap partitions on Linux.
        /// </summary>
        LinuxSwap = 0x82,

        /// <summary>
        /// Generic ID for Windows filesystems, also covers OS/2's HPFS and exFAT.
        /// </summary>
        Windows = 0x07,

        /// <summary>
        /// The Windows 95/98 FAT32 partition type.
        /// </summary>
        Fat32Chs = 0x0B,

        /// <summary>
        /// Same as <see cref="Fat32Chs"/>, but uses LBA instead of CHS.
        /// </summary>
        Fat32Lba = 0x0C,

        /// <summary>
        /// Partition type used by modern UEFI to boot modern operating systems.
        /// </summary>
        EfiSystemPartition = 0xEF,

        /// <summary>
        /// Partition type that tells legacy tools to ignore the disk because it uses GPT, not MBR
        /// </summary>
        GptProtectiveMbr = 0xEE
    }
}
