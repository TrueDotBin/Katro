using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
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
            if (!FileSystemManager.EnsureFilesystem())
                return (int)CommandReturnCode.Success;

            try
            {
                var currentDir = Directory.GetCurrentDirectory();
                Console.WriteLine(currentDir);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
