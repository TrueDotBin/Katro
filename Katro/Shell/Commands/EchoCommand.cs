using Katro.Constants;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// echo [message]
    /// </summary>
    public class EchoCommand : Command
    {
        public override string Name { get; } = "echo";

        public override string Description { get; } = "Displays a message.";

        public override string Usage { get; } = "echo [message]";

        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument(0)
        ];

        public override int Run()
        {
            if (TryGetPositional(0, out var message))
                Console.WriteLine(message.Value);
            else
                Console.WriteLine();

            return (int)CommandReturnCode.Success;
        }
    }
}
