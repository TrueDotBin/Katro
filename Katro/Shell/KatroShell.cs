using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell
{
    /// <summary>
    /// The built-in shell for Katro.
    /// </summary>
    public sealed class KatroShell
    {
        /// <summary>
        /// Initializes the shell by registering commands and the shell prompt.
        /// </summary>
        public void Initialize()
        {
            // TODO: Implement this
        }

        /// <summary>
        /// Runs the input loop once.
        /// </summary>
        public void Run()
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            var (name, context) = ShellParser.Parse(input);
            context?.Shell = this;
        }
    }
}
