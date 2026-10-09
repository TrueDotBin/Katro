using Katro.Enums;
using Katro.Shell.Parsing;

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
        public override string[] Aliases { get; } = ["print", "write"];
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("message", 0, "The message to display", false)
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
