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
        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// This method currently does nothing.
        /// </remarks>
        public static void Init() { }

        /// <summary>
        /// Runs the shell once.
        /// </summary>
        public static void Run()
        {
            if (!Kernel.DontUseFilesystem)
            {
                var current = Directory.GetCurrentDirectory();
                Console.Write($"{current} > ");
            }
            else
            {
                Console.Write("Katro > ");
            }

            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            Console.WriteLine($"Input: {input}");
        }
    }
}
