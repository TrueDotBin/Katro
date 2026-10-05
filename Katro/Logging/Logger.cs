using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Logging
{
    /// <summary>
    /// Logs messages to the terminal.
    /// </summary>
    public static class Logger
    {
        private static void LogBase(ConsoleColor color, string prefix, string message)
        {
            Console.ForegroundColor = color;
            Console.Write($"[ {prefix} ] ");
            Console.ResetColor();

            Console.WriteLine(message);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Info(string message)
            => LogBase(ConsoleColor.Cyan, "INF", message);

        /// <summary>
        /// Logs a message that indicates a successful operation.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Ok(string message)
            => LogBase(ConsoleColor.Green, "SUC", message);

        /// <summary>
        /// Logs a message that may hint at possible failures.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Warn(string message)
            => LogBase(ConsoleColor.Yellow, "WRN", message);

        /// <summary>
        /// Logs a message that indicates a failure.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Error(string message)
            => LogBase(ConsoleColor.Red, "ERR", message);
    }
}
