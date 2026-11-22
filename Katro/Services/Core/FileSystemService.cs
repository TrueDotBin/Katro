using Katro.FileSystem;
using Katro.Services.Abstractions;

namespace Katro.Services.Core
{
    /// <summary>
    /// Ensures that the file system is initialized.
    /// </summary>
    public class FileSystemService : KernelService
    {
        /// <inheritdoc/>
        public override string Name { get; } = "File System";

        /// <inheritdoc/>
        public override string Id { get; } = "fs";

        /// <inheritdoc/>
        public override bool Start()
        {
            FileSystemManager.Initialize();

            if (FileSystemManager.Initialized)
            {
                Directory.CreateDirectory(Paths.EtcDirPath);
                Directory.CreateDirectory(Paths.HomeDirPath);
            }

            return FileSystemManager.Initialized;
        }
    }
}
