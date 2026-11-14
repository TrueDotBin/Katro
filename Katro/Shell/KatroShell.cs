using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.Shell
{
    /// <summary>
    /// The built-in shell for Katro.
    /// </summary>
    public sealed class KatroShell
    {
        private static Logger s_logger = new("Katro Shell");
            
        /// <summary>
        /// The list of registered commands.
        /// </summary>
        public List<Command> Commands { get; private set; } = [];

        private void WritePrompt()
        {
            var startColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("katro");
            Console.ForegroundColor = startColor;

            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Directory.GetCurrentDirectory());
            Console.ForegroundColor = startColor;

            Console.Write("> ");
        }

        /// <summary>
        /// Initializes the shell by registering commands.
        /// </summary>
        public void Initialize()
        {
            Commands.Add(new Help());
        }

        /// <summary>
        /// Runs the input loop once.
        /// </summary>
        public void Run()
        {
            WritePrompt();
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            var (name, context) = ShellParser.Parse(input);

            if (context is null)
            {
                s_logger.Log(LogLevel.Error, "Failed to create command context");
                return;
            }

            var cmd = Commands.FirstOrDefault(c => c.Name == name);

            if (cmd is null)
            {
                s_logger.Log(LogLevel.Error, $"Command {name} not found");
                return;
            }

            context.Shell = this;
            var code = cmd.Execute(context);

            if (code != 0)
                s_logger.Log(LogLevel.Error, $"Command {name} exited with non-success exit code: {code}");
        }
    }
}
