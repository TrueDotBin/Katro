using Cosmos.Kernel.System;
using Katro.Enums;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class RebootCommand : Command
    {
        public override string Name { get; } = "reboot";
        public override string[] Aliases { get; } = ["restart"];
        public override string Description { get; } = "Powers off the system, and then powers it back on.";
        public override string Usage { get; } = "reboot";

        public override int Run()
        {
            Power.Reboot();
            return (int)CommandReturnCode.Success;
        }
    }
}
