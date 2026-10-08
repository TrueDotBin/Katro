using Katro.Constants;
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
        /// Initializes the shell.
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// Runs the shell once.
        /// </summary>
        public static void Run()
        {
            WritePrompt();
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            
        }
    }
}
