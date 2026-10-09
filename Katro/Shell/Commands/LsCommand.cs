using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class LsCommand : Command
    {
        public override string Name { get; } = "ls";
        public override string[] Aliases { get; } = ["lsdir", "list", "dir"];
        public override string Description { get; } = "Lists the provided directory.";
        public override string Usage { get; } = "ls [path]";
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the directory to list.", false)
        ];

        public override int Run()
        {
            if (!FileSystemManager.EnsureFilesystem())
                return (int)CommandReturnCode.Success;

            var path = Directory.GetCurrentDirectory();

            if (TryGetPositional(0, out var pos))
            {
                var value = pos.Value?.ToString();

                if (string.IsNullOrEmpty(value))
                    return (int)CommandReturnCode.BadArgument;

                path = value;
            }

            if (!Directory.Exists(path))
                return (int)CommandReturnCode.DirNotFound;

            try
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);

                foreach (var dir in dirs)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[DIR] ");
                    Console.ResetColor();

                    var dirName = Path.GetFileName(dir);
                    Console.WriteLine(dirName);
                }

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    Console.WriteLine($"      {fileName}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
