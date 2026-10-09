using Katro.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class ClearCommand : Command
    {
        public override string Name { get; } = "clear";
        public override string Description { get; } = "Clears the screen.";
        public override string[] Aliases { get; } = ["cls", "clearscreen"];
        public override string Usage { get; } = "clear";

        public override int Run()
        {
            Console.Clear();
            return (int)CommandReturnCode.Success;
        }
    }
}
