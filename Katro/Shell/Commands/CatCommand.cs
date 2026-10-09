using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class CatCommand : Command
    {
        public override string Name { get; } = "cat";
        public override string[] Aliases { get; } = ["readfile", "readf"];
        public override string Description { get; } = "Reads a file.";
        public override string Usage { get; } = "cat <path>";
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the file to read.")
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

            if (!File.Exists(path))
                return (int)CommandReturnCode.FileNotFound;

            try
            {
                var contents = File.ReadAllText(path);
                Console.WriteLine(contents);
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
