using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class RmCommand : Command
    {
        public override string Name { get; } = "rm";
        public override string[] Aliases { get; } = ["del"];
        public override string Description { get; } = "Removes a file or directory.";
        public override string Usage { get; } = "rm [options] <path>";
        public override CommandArgument[] Args { get; } =
        [
            new OptionArgument("recursive", "r", "Whether to delete a directory recursively, this does nothing if \"path\" is a file", false),
            new PositionalArgument("path", 0, "The path of the file or directory.")
        ];

        public override int Run()
        {
            if (!FileSystemManager.EnsureFilesystem())
                return (int)CommandReturnCode.Success;

            if (!TryGetPositional(0, out var pathArg))
                return (int)CommandReturnCode.NoArguments;

            var recursive = false;
            if (TryGetOption("recursive", out var opt))
                recursive = (bool?)opt.Value ?? false;

            var path = pathArg.Value?.ToString();

            if (string.IsNullOrEmpty(path))
                return (int)CommandReturnCode.BadArgument;

            try
            {
                if (File.Exists(path))
                    File.Delete(path);
                else if (Directory.Exists(path))
                    Directory.Delete(path, recursive);
                else
                    return (int)CommandReturnCode.GeneralFailure;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return (int)CommandReturnCode.GeneralFailure;
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
