using Katro.Constants.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.FileSystem
{
    /// <summary>
    /// Paths for files and directories.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// The directory where other users' directories are located.
        /// </summary>
        public static readonly string HomeDirPath
            = Path.Combine(MountPoints.RootPartition, "home");

        /// <summary>
        /// The directory where Katro's configuration files (user database, etc.) are located.
        /// </summary>
        public static readonly string EtcDirPath
            = Path.Combine(MountPoints.RootPartition, "etc");

        /// <summary>
        /// The file where Katro stores global configuration.
        /// </summary>
        public static readonly string ConfigFilePath
            = Path.Combine(MountPoints.RootPartition, "config.cfg");

        /// <summary>
        /// The file where Katro stores all users.
        /// </summary>
        public static readonly string UserDatabaseFilePath
            = Path.Combine(EtcDirPath, "users");
    }
}
