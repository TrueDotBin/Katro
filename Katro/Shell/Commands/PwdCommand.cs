using Katro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class PwdCommand : Command
    {
        public override string Name { get; } = "pwd";
        public override string Description { get; } = "Displays the current directory.";
        public override string Usage { get; } = "pwd";

        public override int Run()
        {
            if (Kernel.DontUseFilesystem)
            {
                Console.WriteLine("You aren't using the file system!");
                Console.WriteLine("Reboot and select \"Katro (no file system)\" from the boot menu.");

                return (int)CommandReturnCode.Success;
            }

            var currentDir = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDir);

            return (int)CommandReturnCode.Success;
        }
    }
}
