using Cosmos.Kernel.System.Audio;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.Audio
{
    /// <summary>
    /// Plays a specified audio file.
    /// </summary>
    public sealed class Play : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "play";

        /// <inheritdoc/>
        public override string Description { get; } = "Plays a specified audio file.";

        /// <inheritdoc/>
        public override string Usage { get; } = "play <file>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Audio;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
                return CommandReturnCode.MissingArgument;

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            if (!File.Exists(path))
                return CommandReturnCode.NoEntry;

            var bytes = File.ReadAllBytes(path);
            var audioStream = MemoryAudioStream.FromWave(bytes);

            if (!AudioManager.Play(audioStream))
            {
                Terminal.WriteLine("Failed to play audio file");
                return CommandReturnCode.GeneralFailure;
            }

            return CommandReturnCode.Success;
        }
    }
}
