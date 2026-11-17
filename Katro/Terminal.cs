using System;
using System.Collections.Generic;
using System.Text;

namespace Katro
{
    /// <summary>
    /// Utilities for printing fancy messages.
    /// </summary>
    public static class Terminal
    {
        private static (ConsoleColor PrevForeground, ConsoleColor PrevBackground) SetColor(ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            Console.ForegroundColor = fg ?? oldFg;
            Console.BackgroundColor = bg ?? oldBg;

            return (oldFg, oldBg);
        }

        /// <summary>
        /// Displays a formatted heading.
        /// </summary>
        /// <param name="label">What this heading grpups.</param>
        public static void Heading(object label)
        {
            WriteColored("===== ", ConsoleColor.Gray);
            WriteColored(label);
            WriteLineColored(" =====", ConsoleColor.Gray);
        }

        /// <summary>
        /// Displays a formatted information line.
        /// </summary>
        /// <param name="label">The description label.</param>
        /// <param name="value">The value to display.</param>
        public static void InfoLine(string label, object value)
        {
            WriteColored($"{label,-30}", ConsoleColor.Yellow);
            WriteLineColored(value, ConsoleColor.Gray);
        }

        /// <summary>
        /// Primts a value with the specified foreground and background, with a trailing new line.
        /// </summary>
        /// <param name="value">The value that should be written into the console.</param>
        /// <param name="fg">The color of the text.</param>
        /// <param name="bg">The color of the text's background.</param>
        public static void WriteLineColored(object value, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var (oldFg, oldBg) = SetColor(fg, bg);

            Console.WriteLine(value);

            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Primts a value with the specified foreground and background.
        /// </summary>
        /// <param name="value">The value that should be written into the console.</param>
        /// <param name="fg">The color of the text.</param>
        /// <param name="bg">The color of the text's background.</param>
        public static void WriteColored(object value, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var (oldFg, oldBg) = SetColor(fg, bg);

            Console.Write(value);

            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Displays a formatted C# exception.
        /// </summary>
        /// <param name="ex">The exception that should be displayed.</param>
        public static void CSharpException(Exception ex)
        {
            WriteLineColored(ex.Message, ConsoleColor.Red);

            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                Console.WriteLine();

                Heading("Stack Trace");
                WriteLineColored(ex.StackTrace, ConsoleColor.Red);
            }
        }
    }
}
