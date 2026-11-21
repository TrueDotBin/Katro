using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
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
        public override string Usage { get; } = "ls [--long] [path]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
                path = ctx.Shell.CurrentDirectory;
            else
                path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            if (!Directory.Exists(path))
                return CommandReturnCode.NoEntry;

            var longOutput = ctx.HasFlag("long");

            var dirInfo = new DirectoryInfo(path);

            var entries = dirInfo.EnumerateFileSystemInfos();
            if (!entries.Any())
            {
                Terminal.WriteLine("(empty)", ConsoleColor.Gray);
                return CommandReturnCode.Success;
            }

            foreach (var entry in entries)
            {
                var name = entry.Name + (entry is DirectoryInfo ? "/" : "");

                if (longOutput)
                {
                    var typeColor = entry is DirectoryInfo ? ConsoleColor.Yellow : ConsoleColor.White;
                    var size = entry is FileInfo f ? FileSizeFormatter.FormatSize((ulong)f.Length) : "-";
                    var date = entry.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                    Terminal.WriteLine($"{date} {size,8} {name}", typeColor);
                }
                else
                {
                    Terminal.WriteLine(name);
                }
            }

            return CommandReturnCode.Success;
        }
    }
}
