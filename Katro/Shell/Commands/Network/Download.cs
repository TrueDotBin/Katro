using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;
using Cosmos.Network.Http;
using Katro.FileSystem;
using Katro.Users;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Downloads a file.
    /// </summary>
    public sealed class Download : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "download";

        /// <inheritdoc/>
        public override string Description { get; } = "Downloads a file.";

        /// <inheritdoc/>
        public override string Usage { get; } = "download <url> [--output=file]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Network;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var url = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(url))
                return CommandReturnCode.MissingArgument;

            url = UrlHelper.Normalize(url);

            if (!ctx.TryGetOption("output", out var output))
            {
                var absolutePath = new Uri(url).AbsolutePath;
                output = Path.GetFileName(absolutePath);

                if (string.IsNullOrEmpty(output))
                    output = "web-download";

                output = PathHelper.Resolve(UserManager.CurrentUser.ExtraDirs[UserFolder.Downloads], output);
            }
            else
            {
                output = PathHelper.Resolve(ctx.Shell.CurrentDirectory, output);
            }

            var withoutExtension = Path.GetFileNameWithoutExtension(output);
            var extension = Path.GetExtension(output);

            var basePath = Path.GetDirectoryName(output);
            if (string.IsNullOrEmpty(basePath))
                return CommandReturnCode.GeneralFailure;

            var dest = output;
            var counter = 1;

            while (File.Exists(dest))
            {
                var fileName = $"{withoutExtension} ({counter++}){extension}";
                dest = Path.Combine(basePath, fileName);
            }

            using var stream = HttpManager.Http.GetStream(url);
            using var fs = new FileStream(dest, FileMode.CreateNew);
            stream.CopyTo(fs);

            Terminal.WriteLine($"Saved {url} to {dest}");

            return CommandReturnCode.Success;
        }
    }
}
