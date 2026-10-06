using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Constants
{
    /// <summary>
    /// Constants for error codes.
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// Error code used whenever the file system fails to register.
        /// </summary>
        public const string FailedToRegisterFileSystem =
            "FAILED_TO_REGISTER_FILESYSTEM";

        /// <summary>
        /// Error code used whenever the system fails to create the root partition.
        /// </summary>
        public const string FailedToCreateRootPartition =
            "FAILED_TO_CREATE_ROOT_PARTITION";

        /// <summary>
        /// Error code used whenever the system fails to mount the root partition.
        /// </summary>
        public const string FailedToMountRootPartition =
            "FAILED_TO_MOUNT_ROOT_PARTITION";

        /// <summary>
        /// Error code used whenever the system fails to find any partitions.
        /// </summary>
        public const string NoPartitions =
            "NO_PARTITIONS_FOUND";

        /// <summary>
        /// Error code used whenever the system doesn't detect a usable disk.
        /// </summary>
        public const string NoDisk =
            "NO_USABLE_DISK_FOUND";
    }
}
