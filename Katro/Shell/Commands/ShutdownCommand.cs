using Cosmos.Kernel.System;
using Katro.Enums;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class ShutdownCommand : Command
    {
        public override string Name { get; } = "shutdown";
        public override string[] Aliases { get; } = ["poweroff"];
        public override string Description { get; } = "Powers off the system.";
        public override string Usage { get; } = "shutdown";

        public override int Run()
        {
            Power.Shutdown();
            return (int)CommandReturnCode.Success;
        }
    }
}
