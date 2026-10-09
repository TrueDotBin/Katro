using Katro.Enums;
using Katro.Logging;
using Katro.Shell.Commands;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell
{
    /// <summary>
    /// The Katro shell, also known as "ktsh".
    /// </summary>
    public static class KatroShell
    {
        private static void WritePrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Katro ");
            Console.ResetColor();

            if (!Kernel.DontUseFilesystem)
            {
                var current = Directory.GetCurrentDirectory();

                Console.Write("[ ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(current);
                Console.ResetColor();

                Console.Write(" ] ");
            }

            Console.Write($":> "); // its a smiley face!!
        }

        /// <summary>
        /// Runs a command.
        /// </summary>
        /// <param name="input">The input to use.</param>
        /// <returns>The command's return code.</returns>
        public static int Run(string input)
        {
            var cmd = ArgumentParser.Parse(input);

            if (cmd == null)
                return (int)CommandReturnCode.CommandNotFound;

            var code = cmd.Run();

            foreach (var arg in cmd.Args)
                arg.ResetValueToDefault();

            return code;
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        public static void Init()
        {
            CommandManager.Register(new EchoCommand());
            CommandManager.Register(new HelpCommand());
            CommandManager.Register(new PwdCommand());
            CommandManager.Register(new CdCommand());
            CommandManager.Register(new LsCommand());
            CommandManager.Register(new MkdirCommand());
            CommandManager.Register(new ClearCommand());
        }

        /// <summary>
        /// Runs the shell once.
        /// </summary>
        public static void Run()
        {
            WritePrompt();
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            var code = Run(input);

            switch (code)
            {
                case (int)CommandReturnCode.CommandNotFound:
                    Logger.Error("Command not found");
                    break;

                case (int)CommandReturnCode.GeneralFailure:
                    Logger.Error("The command failed");
                    break;

                case (int)CommandReturnCode.NoArguments:
                    Logger.Error("No arguments provided");
                    break;

                case (int)CommandReturnCode.BadArgument:
                    Logger.Error("Invalid argument(s)");
                    break;

                case (int)CommandReturnCode.DirNotFound:
                    Logger.Error("Directory not found");
                    break;

                case (int)CommandReturnCode.FileNotFound:
                    Logger.Error("File not found");
                    break;

                default:
                    break;
            }
        }
    }
}
