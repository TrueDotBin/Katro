using Katro.Enums;
using Katro.FileSystem;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class MkdirCommand : Command
    {
        public override string Name { get; } = "mkdir";
        public override string Description { get; } = "Creates a new directory.";
        public override string Usage { get; } = "mkdir <path>";
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the directory to create.", true)
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

            Directory.CreateDirectory(path);

            return (int)CommandReturnCode.Success;
        }
    }
}
