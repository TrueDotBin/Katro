using Katro.Enums;
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
            if (Kernel.DontUseFilesystem)
            {
                Console.WriteLine("You aren't using the file system!");
                Console.WriteLine("Reboot and select \"Katro (no file system)\" from the boot menu.");

                return (int)CommandReturnCode.Success;
            }

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

            var dirs = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            foreach (var dir in dirs)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[DIR] ");
                Console.ResetColor();

                var dirName = Path.GetDirectoryName(dir);
                Console.WriteLine(dirName);
            }

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                Console.WriteLine($"      {fileName}");
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
