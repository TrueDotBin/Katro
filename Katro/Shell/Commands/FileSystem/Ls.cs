using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Shell.Commands.FileSystem
{
    /// <summary>
    /// Lists all files and directories.
    /// </summary>
    public class Ls : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "ls";

        /// <inheritdoc/>
        public override string Description { get; } = "Lists all files and directories.";

        /// <inheritdoc/>
        public override string Usage { get; } = "ls [path]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
                path = ctx.Shell.CurrentDirectory;

            if (!Directory.Exists(path))
                return CommandReturnCode.NoEntry;

            var dirInfo = new DirectoryInfo(path);

            var entries = dirInfo.EnumerateFileSystemInfos();
            if (!entries.Any())
            {
                Terminal.WriteLineColored("(empty)", ConsoleColor.Gray);
                return CommandReturnCode.Success;
            }

            foreach (var entry in entries)
            {
                var typeColor = entry is DirectoryInfo ? ConsoleColor.Yellow : ConsoleColor.White;
                var size = entry is FileInfo f ? FileSizeFormatter.FormatSize((ulong)f.Length) : "-";
                var name = entry.Name + (entry is DirectoryInfo ? "/" : "");

                Terminal.WriteLineColored($"{size,8} {name}", typeColor);
            }

            return CommandReturnCode.Success;
        }
    }
}
