using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class CdCommand : Command
    {
        public override string Name { get; } = "cd";
        public override string[] Aliases { get; } = ["chdir"];
        public override string Description { get; } = "Changes the current directory.";
        public override string Usage { get; } = "pwd <path>";
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the directory.")
        ];

        public override int Run()
        {
            if (!FileSystemManager.EnsureFilesystem())
                return (int)CommandReturnCode.Success;

            if (!TryGetPositional(0, out var pathArg))
                return (int)CommandReturnCode.NoArguments;

            var path = pathArg.Value?.ToString();

            if (string.IsNullOrEmpty(path))
                return (int)CommandReturnCode.BadArgument;

            if (!Directory.Exists(path))
                return (int)CommandReturnCode.DirNotFound;

            try
            {
                Directory.SetCurrentDirectory(path);
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
